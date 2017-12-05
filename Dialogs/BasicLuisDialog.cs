using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace Microsoft.Bot.Sample.LuisBot
{
    // For more information about this template visit http://aka.ms/azurebots-csharp-luis
    [Serializable]
    public class BasicLuisDialog : LuisDialog<object>
    {
        public BasicLuisDialog() : base(new LuisService(new LuisModelAttribute(ConfigurationManager.AppSettings["LuisAppId"], ConfigurationManager.AppSettings["LuisAPIKey"])))
        {
        }

        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Sorry I don't understand: '{result.Query}'. Could you please rephrase yourself? Type 'Help' for assistance"); //
            context.Wait(MessageReceived);
        }

        [LuisIntent("Welcome")]
        public async Task WelcomeIntent(IDialogContext context, LuisResult result)
        {
            /*foreach (var entity in result.Entities)
            {
                 if ( entity.Type.ToUpper() == "THANK" )
                {
                    await context.PostAsync($"You are welcome!"); //
                    context.Wait(MessageReceived);
                    return;
                }
                
            } */ 
                   await context.PostAsync($"Hello!"); //
                   context.Wait(MessageReceived);
        }
        
         [LuisIntent("Introduction")]
        public async Task IntroductionIntent(IDialogContext context, LuisResult result)
        {
            foreach (var entity in result.Entities) 
            {
                var value = entity.Entity.ToUpper();
                if ( entity.Type.ToUpper() == "INTRO" )
                {
                    if ( value == "WHO ARE YOU")
                    {
                        await context.PostAsync("Hi I am a Chatbot for Supermarket.");
                        context.Wait(MessageReceived);
                        return;
                    }
                    else if (value == "WHO CREATED YOU")
                    {
                        await context.PostAsync("I was developed at Foodstuffs, New Zealand");
                        context.Wait(MessageReceived);
                        return;
                    }
                    else if (value =="WHAT CAN YOU DO")
                    {
                        await context.PostAsync("I can help you in shopping!");
                        context.Wait(MessageReceived);
                    return;
                    }
                }
            }   
        }    
       
        [LuisIntent("CustomerQuery")]
        public async Task CustomerQueryIntent(IDialogContext context, LuisResult result)
        {
            foreach (var entity in result.Entities)
            {
                var value = entity.Entity.ToUpper();
                if (entity.Type.ToUpper() == "PRODUCTINFO")
                {
                    await context.PostAsync("Would you like to get the details?"); //
                    context.Wait(MessageReceived);
                    return;
                }
                else if (entity.Type.ToUpper() == "BuyItem")
                {
                    await context.PostAsync("Sorry, we don't have it. Would you like to see other available option?"); //
                    context.Wait(MessageReceived);
                    return;
                }
            }
                await context.PostAsync("I can shop for you");//
                context.Wait(MessageReceived);
           }      
           
         [LuisIntent("HelpCustomer")]
         public async Task HelpCustomerIntent(IDialogContext context, LuisResult result)
         {
            await context.PostAsync("Hi! Try asking me things like 'I want to buy 2 kg apples' or 'Buy me some fruits' or 'what do you have'"); //
            context.Wait(MessageReceived);
         }
     }
}