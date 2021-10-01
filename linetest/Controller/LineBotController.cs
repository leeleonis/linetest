using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using isRock.LineBot;
using Line.Messaging;
using linetest.Helper;
using linetest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace linetest
{
    [Route("api/linebot")]
    public class LineBotController : LineWebHookControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpContext _httpContext;
        private readonly LineBotConfig _lineBotConfig;
        private readonly ILogger _logger;
        private List<MSTranslator> _MSTranslatorlist;
        //public List<MSTranslator> MSTranslatorlist = new List<MSTranslator>();

        public LineBotController(IServiceProvider serviceProvider, LineBotConfig lineBotConfig, ILogger<LineBotController> logger, List<MSTranslator> MSTranslatorlist)
        {
            _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            _httpContext = _httpContextAccessor.HttpContext;
            _lineBotConfig = lineBotConfig;
            _logger = logger;
            _MSTranslatorlist = MSTranslatorlist;
        }

        private string GetState(string UserID)
        {
            var MSTranslator= _MSTranslatorlist.Where(x => x.ID == UserID).FirstOrDefault();
            if (MSTranslator == null)
            {
                return "";
            }
            else
            {
                return MSTranslator.language;
            }      
        }

        private void SetState(string UserID, string LanguageCode)
        {
            var MSTranslator = _MSTranslatorlist.Where(x => x.ID == UserID).FirstOrDefault();
            if (MSTranslator == null)
            {
                _MSTranslatorlist.Add(new MSTranslator
                {
                    ID = UserID,
                    language = LanguageCode

                });
            }
            else
            {
                MSTranslator.language = LanguageCode;
            }
        }


        //完整的路由網址就是 https://xxx/api/linebot/run
        [HttpPost("run")]
        public IActionResult Post()
        {
            var AdminUserId = "U8dd73688cf9bd43782e74083c6c372f7";

            try
            {
                //設定ChannelAccessToken
                this.ChannelAccessToken = _lineBotConfig.accessToken;
                //取得Line Event

                var LineEvent = this.ReceivedMessage.events.FirstOrDefault();
                //配合Line verify 
                if (LineEvent == null) return Ok();
                if (LineEvent.replyToken == "00000000000000000000000000000000") return Ok();
                var responseMsg = "";
                //準備回覆訊息
                if (LineEvent.type.ToLower() == "message")
                {
                    if (LineEvent.message.type == "text")
                    {
                        //GoogleShee.SaveMsg(LineEvent.message.text);
                        var ImgTemplist = MyFun.ImgTemplist();
                        if (LineEvent.message.text.StartsWith("/"))
                        {
                            if (LineEvent.message.text == "/help")
                            {
                                responseMsg = "目前可用關鍵字" + Environment.NewLine + string.Join(Environment.NewLine, ImgTemplist.Select(x => x.Key)) + Environment.NewLine + "/選單" + Environment.NewLine + "/翻成XX" + Environment.NewLine + "/語系";
                                this.ReplyMessage(LineEvent.replyToken, responseMsg);
                            }
                            else if (LineEvent.message.text == "/選單")
                            {
                                var actions = new List<TemplateActionBase>();
                                actions.Add(new MessageAction() //MessageAction
                                {
                                    label = "倒了",
                                    text = "已經倒了"
                                });
                                actions.Add(new MessageAction() //MessageAction
                                {
                                    label = "快倒了",
                                    text = "看來是快要倒了"
                                });
                                actions.Add(new MessageAction() //MessageAction
                                {
                                    label = "命不久已",
                                    text = "唉唉唉"
                                });
                                var btn = new isRock.LineBot.ButtonsTemplate
                                {
                                    title = "公司倒了嗎",
                                    text = "我只是想問公司的情況",
                                    thumbnailImageUrl = new Uri("https://i.imgur.com/mm1JcL0.jpg"),
                                    actions = actions


                                };
                                this.ReplyMessage(LineEvent.replyToken, btn);
                                //return Ok();
                            }
                            else if (LineEvent.message.text == "/語系")
                            {
                                var Languagelist = MyFun.Languagelist().Select(x => x.Key);
                                responseMsg = "目前可翻譯語系" + Environment.NewLine + string.Join(Environment.NewLine, Languagelist);
                                this.ReplyMessage(LineEvent.replyToken, responseMsg);
                            }
                            else if (LineEvent.message.text.StartsWith("/翻成"))
                            {
                                var response = ProcessCommand(LineEvent);
                                if (response != null)
                                {
                                    this.ReplyMessage(LineEvent.replyToken, response);
                                }
                            }
                            else if (LineEvent.message.text == "/停止翻譯")
                            {

                                SetState(LineEvent.source.userId, "");
                                responseMsg = "好的，已停止翻譯";
                                this.ReplyMessage(LineEvent.replyToken, responseMsg);
                            }
                            else
                            {

                                var ImgTemp = ImgTemplist.Where(x => x.Key == LineEvent.message.text).FirstOrDefault();
                                if (ImgTemp != null)
                                {
                                    this.ReplyMessage(LineEvent.replyToken, ImgTemp.uri);
                                }
                                //responseMsg = $"你說了: {LineEvent.message.text}";
                            }
                        }
                        else
                        {
                            var MSTranslator = _MSTranslatorlist.Where(x => x.ID == LineEvent.source.userId).FirstOrDefault();
                            if (MSTranslator != null && !string.IsNullOrEmpty(MSTranslator.language))
                            {
                                MSTranslatorUtility.MSTranslatorTextKey = _lineBotConfig.MSTranslatorTextKey;
                                var result = MSTranslatorUtility.Translate(LineEvent.message.text.Trim(), GetState(LineEvent.source.userId));
                                this.ReplyMessage(LineEvent.replyToken, result.FirstOrDefault().translations.FirstOrDefault().text);
                            }
                        }

                    }
                    else if (LineEvent.message.type == "image")
                    {


                    }
                    else
                    {
                        //this.ReplyMessage(LineEvent.replyToken, 11538, 51626501);
                    }
                }
                else
                {

                    //responseMsg = $"收到 event : {LineEvent.type} ";
                }
                //回覆訊息
                //this.ReplyMessage(LineEvent.replyToken, responseMsg);
                //response OK
                return Ok();
            }
            catch (Exception ex)
            {
                //回覆訊息
                this.PushMessage(AdminUserId, "發生錯誤:\n" + ex.Message);
                //response OK
                return Ok();
            }
        }
        [HttpPost("runo")]
        public async Task<IActionResult> Posto()
        {
            try
            {
                //throw new Exception("出錯了~出錯了~");
                var events = await _httpContext.Request.GetWebhookEventsAsync(_lineBotConfig.channelSecret);
                var lineMessagingClient = new LineMessagingClient(_lineBotConfig.accessToken);
                var lineBotApp = new LineBotApp(lineMessagingClient);
                await lineBotApp.RunAsync(events);
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonConvert.SerializeObject(ex));
            }
            return Ok();
        }
        private isRock.LineBot.MessageBase ProcessCommand(Event LineEvent)
        {
            var language = LineEvent.message.text.Trim().Replace("/翻成", "");
            var SupportLanguage = MyFun.Languagelist();
            var ret = from c in SupportLanguage
                      where c.Key == language
                      select c;
            if (ret.Count() > 0)
            {
                SetState(LineEvent.source.userId, ret.FirstOrDefault().Value);
                var ResponseMsg = new isRock.LineBot.TextMessage($"好的，後面你輸入的句子我會開始翻譯成{ret.FirstOrDefault().Key}，如要停止翻譯 /停止翻譯");
                return ResponseMsg;
            }
            else
            {
                var ResponseMsg = new isRock.LineBot.TextMessage($"喔喔，我還認不得你要的語言 - {language}");
                return ResponseMsg;
            }
        }
    }
}

