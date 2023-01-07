# What is Hosted Service?

  The hosted service is a one project that can to run a multiple actions in background using a delay time. We can use that to run for example:
  
    - Create routines to execute any procedure, with web scrapping, feth data on database.
    - Send notifications from time to time.
    - Generate data sumary.

# Hosted Service documentation

  If you want to learn more about this service: [Microsoft Documentation](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services)
  
# About this project

    - Was created a hosted service to get a dolar price using a external API using a 1 second of delay. So, one by one second this service get a current dolar value and send to database in background. 
  
    - This hosted service have too a second routine on this same hosted service: We created a routine to get a average dolar price and send this value to whatsapp message using a external API. And in this case, we used a 60 seconds to delay. So, 60 by 60 seconds this service send a message to whatsapp.
  
# How to use this hosted service

   You need to create a account on [Whatsapp Api](https://user.ultramsg.com), after that, please create a api instance on this api (the tutorial is there) and add your whatsapp scan in created instance.
  
   After that, you need to create a database on your [environment](CreateDatabase.sql).
  
# Configuration

  After the create you whatsapp instance and create your database and table, let's go see a little about the this project.
  
   - To begin, it needs add the two routine that was created: the get dolar value and save to database and get de average value and send to whatsapp. [Go To](../HostedService/WorkerJobs/Program.cs)

   - After that, you need change your string connection on this file: [Go to](../HostedService/WorkerJobs.DataAccess/CurrencyValue/CurrencyValue.cs)
  
   - Now you need to use your instance informations about your Whatsapp Api that was created on [Whatsapp Api](https://user.ultramsg.com) in file. [Go To](../HostedService/WorkerJobs.DataAccess/CurrencyValue/WhatsAppApi.cs)
  
   - The last step you need to add your number phone that was scanned on WhatsApp Api. [Go To](../HostedService/WorkerJobs/Workers/CurrencyValueToWhatsAppWorker.cs)
  
# The Result
  
  Now we need see the result on console about the routine progress. [Go To](Images/console.jpeg)

  And your whatsapp should have something like this: [Go To](Images/zap.jpeg)

