//using System;
//using System.Threading.Tasks;
//using Auth0.AuthenticationApi;
//using Auth0.AuthenticationApi.Models;
//using Auth0.AspNet;
//using System.Collections.Generic;
//using System.Configuration;
//using System.IdentityModel.Services;
//using System.Linq;
//using System.Web;
//using Auth0.Core;
//using Socha3.Common.AppEnvironment;
//using Socha3.MemeBox2000.Controllers;
//using Auth0;

//namespace Socha3.MemeBox2000
//{
//    public class LoginCallback : HttpTaskAsyncHandler
//    {
//        public override async Task ProcessRequestAsync(HttpContext context)
//        {
//            var uri = new Uri($"https://{AppEnv.AppSettings["auth0:Domain"]}");
//            AuthenticationApiClient client = new AuthenticationApiClient(uri);

//            var token = await client.GetTokenAsync(new AuthorizationCodeTokenRequest
//            {
//                ClientId = ConfigurationManager.AppSettings["auth0:ClientId"],
//                ClientSecret = ConfigurationManager.AppSettings["auth0:ClientSecret"],
//                Code = context.Request.QueryString["code"],
//                RedirectUri = context.Request.Url.ToString()                
//            });

//            var profile = await client.GetUserInfoAsync(token.AccessToken);

//            var user = new List<KeyValuePair<string, object>>
//            {
//                new KeyValuePair<string, object>("name", profile.FirstName ?? profile.Email),
//                new KeyValuePair<string, object>("email", profile.Email),
//                new KeyValuePair<string, object>("family_name", profile.LastName),
//                new KeyValuePair<string, object>("given_name", profile.FirstName),
//                new KeyValuePair<string, object>("nickname", profile.NickName),
//                new KeyValuePair<string, object>("picture", profile.Picture),
//                new KeyValuePair<string, object>("user_id", profile.UserId),
//                new KeyValuePair<string, object>("id_token", token.IdToken),
//                new KeyValuePair<string, object>("access_token", token.AccessToken),
//                new KeyValuePair<string, object>("refresh_token", token.RefreshToken),
//                new KeyValuePair<string, object>("connection", profile.AdditionalClaims.FirstOrDefault(kvp => kvp.Key.ToLower().StartsWith("conn")).Value ?? ""),
//                new KeyValuePair<string, object>("provider", profile.ZoneInformation)
//            };

//            // NOTE: Uncomment the following code in order to include claims from associated identities
//            //profile.Identities.ToList().ForEach(i =>
//            //{
//            //    user.Add(new KeyValuePair<string, object>(i.Connection + ".access_token", i.AccessToken));
//            //    user.Add(new KeyValuePair<string, object>(i.Connection + ".provider", i.Provider));
//            //    user.Add(new KeyValuePair<string, object>(i.Connection + ".user_id", i.UserId));
//            //});

//            // NOTE: uncomment this if you send roles
//            // user.Add(new KeyValuePair<string, object>(ClaimTypes.Role, profile.ExtraProperties["roles"]));

//            // NOTE: this will set a cookie with all the user claims that will be converted 
//            //       to a ClaimsPrincipal for each request using the SessionAuthenticationModule HttpModule. 
//            //       You can choose your own mechanism to keep the user authenticated (FormsAuthentication, Session, etc.)
//            FederatedAuthentication.SessionAuthenticationModule.CreateSessionCookie(user);

//            if (context.Request.QueryString["state"] != null && context.Request.QueryString["state"].StartsWith("ru="))
//            {
//                var state = HttpUtility.ParseQueryString(context.Request.QueryString["state"]);
//                context.Response.Redirect(state["ru"], true);
//            }

//            SaveUserInfo(profile);

//            context.Response.Redirect("/");
//        }

//        private void SaveUserInfo(UserInfo profile)
//        {
//            using (var ctx = MemeBox2000BaseController.GetCtx())
//            {
//                var dbUser = ctx.Users.FirstOrDefault(u => u.Email.ToLower() == profile.Email.ToLower());
//                if (dbUser == null)
//                {
//                    ctx.Users.Add(new DataAccess.EF.Domo.Models.User
//                    {
//                        CreatedBy = AppEnv.Identity,
//                        CreatedOn = DateTime.Now,
//                        Email = profile.Email,
//                        FirstName = profile.FirstName,
//                        LastName = profile.LastName,
//                        ModifiedBy = AppEnv.Identity,
//                        ModifiedOn = DateTime.Now,
//                        UserName = profile.UserId
//                    });
//                }
//                else
//                {
//                    dbUser.FirstName = profile.FirstName;
//                    dbUser.LastName = profile.LastName;
//                    dbUser.UserName = profile.UserId;
//                }
//                ctx.SaveChanges();
//            }
//        }

//        public new bool IsReusable
//        {
//            get { return false; }
//        }
//    }
//}