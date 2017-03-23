using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Litdev.Service
{
    /// <summary>
    /// 接口安全验证
    /// </summary>
    public class APIAuthenticationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            APIResponseEntity<int> result = new APIResponseEntity<int>();

            IEnumerable<string> monsterApiKeyHeaderValues = null;
            //验证HTTP报文头
            if (request.Headers.TryGetValues("X-MonsterAccountToken", out monsterApiKeyHeaderValues))
            {
                string oauth = monsterApiKeyHeaderValues.First();
                if (!string.IsNullOrWhiteSpace(oauth))
                {
                    Tools.Crypto3DES des = new Tools.Crypto3DES(ServiceConfig.DES3KEY);
                    string[] vals = des.DESDeCode(oauth).Split('&');
                    if (vals.Length == 3)
                    {
                        string valstr = "lizd@2sfqlyalsd!";
                        if (vals[0].Equals(valstr))
                        {
                            DateTime dt_now = DateTime.Now;
                            DateTime dt_old = Tools.TypeHelper.GetTime(vals[2], dt_now);
                            double diff = Tools.TypeHelper.DateTimeDiff(dt_old, dt_now, "as");
                            int ss = 10;
                            if (diff < ss) //10秒前的数据，则失败
                            {
                                var userNameClaim = new Claim(ClaimTypes.Name, vals[1]);
                                var identity = new ClaimsIdentity(new[] { userNameClaim }, "MonsterAppApiKey");
                                var principal = new ClaimsPrincipal(identity);
                                Thread.CurrentPrincipal = principal;

                                if (System.Web.HttpContext.Current != null)
                                {
                                    System.Web.HttpContext.Current.User = principal;
                                }
                            }
                            else
                            {
                                result.msgbox = "超时";
                                return requestCancel(request, cancellationToken, result);
                            }
                        }
                        else
                        {
                            result.msgbox = "授权数据错误1";
                            return requestCancel(request, cancellationToken, result);
                        }
                    }
                    else
                    {
                        result.msgbox = "授权格式错误";
                        return requestCancel(request, cancellationToken, result);
                    }
                }
                else
                {
                    result.msgbox = "缺少授权参数";
                    return requestCancel(request, cancellationToken, result);
                }
            }
            else
            {
                result.msgbox = "未经授权";
                return requestCancel(request, cancellationToken, result);
            }

            return base.SendAsync(request, cancellationToken);
        }


        /// <summary>
        /// 取消请求
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private Task<HttpResponseMessage> requestCancel(HttpRequestMessage request, CancellationToken cancellationToken, APIResponseEntity<int> result)
        {
            CancellationTokenSource _tokenSource = new CancellationTokenSource();
            cancellationToken = _tokenSource.Token;
            _tokenSource.Cancel();
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.Unauthorized);
            string message = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            response.Content = new StringContent(message, Encoding.GetEncoding("UTF-8"), "application/json");
            return base.SendAsync(request, cancellationToken).ContinueWith(task =>
            {
                return response;
            });
        }
    }
}
