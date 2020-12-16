using Line.Messaging;
using Line.Messaging.Webhooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace linetest
{
    public class LineBotApp : WebhookApplication
    {
        private readonly LineMessagingClient _messagingClient;
        public LineBotApp(LineMessagingClient lineMessagingClient)
        {
            _messagingClient = lineMessagingClient;
        }

        protected override async Task OnMessageAsync(MessageEvent ev)
        {
            var result = null as List<ISendMessage>;

            switch (ev.Message)
            {
                //文字訊息
                case TextEventMessage textMessage:
                    {
                        //頻道Id
                        var channelId = ev.Source.Id;
                        //使用者Id
                        var userId = ev.Source.UserId;

                        var Msg = "";
                        //回傳 hellow
                        if (textMessage.Text.Contains("鴻銡"))
                        {
                            Msg = textMessage.Text + "看來是死定了";
                        } else if (textMessage.Text.Contains("和和"))
                        {
                            Msg = textMessage.Text + "對方還在測試";
                        }
                        else if (textMessage.Text.Contains("威鉅"))
                        {
                            Msg = textMessage.Text + "不明不白的就簽收了";
                        }
                        else if (textMessage.Text.Contains("今天"))
                        {
                            Msg = textMessage.Text + "哈哈哈，今天是TPS和Angular 分享";
                        }
                        else if (textMessage.Text.Contains("開會"))
                        {
                            Msg = textMessage.Text + "我的人生又少了一個小時";
                        }
                        else
                        {
                            Msg =  "我老闆什麼都不會";
                        }
                        result = new List<ISendMessage> { new TextMessage(Msg) };
                        if (result != null)
                        {
                            await _messagingClient.ReplyMessageAsync(ev.ReplyToken, result);
                        }
                        break;
                    }
            }

        }
    }
}
