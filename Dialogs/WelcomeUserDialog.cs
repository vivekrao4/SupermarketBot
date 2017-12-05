using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace Microsoft.Bot.Sample.LuisBot
{
	[Serializable]
    public class WelcomeUserDialog : IDialog<object>
    {
        private const string BuyProductOption = "Buy Products";
        private const string GetHelpOption = "Help";
		
		public async Task StartAsync (IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }
        
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            PromptDialog.Choice(
                context,
                this.AfterSelection,
                new[] {BuyProductOption, GetHelpOption},
                "Hello! How may I assist you?",
                "Hi! Please select one of the given option.",
                attempts:3);
            
        }
        
        private async Task AfterSelection(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                var Selection = await result;
                
                switch(Selection)
                {
                    case BuyProductOption:
                    context.Call(new BasicLuisDialog(), this.AfterShopping);
                    break;
                    
                    case GetHelpOption:
                    context.Call(new BasicLuisDialog(), this.AfterHelp);
                    break;
                    
                }
            }
            
            catch(TooManyAttemptsException)
            {
                await this.StartAsync(context);
            }
        }
        
        private async Task AfterShopping(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("Thank you for shopping with us!");
            await this.StartAsync(context);
        }
        
        private async Task AfterHelp(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("Would you like to start shopping?");
            await this.StartAsync(context);
        }
        
	}
	
    
	
	
	
	
	
}