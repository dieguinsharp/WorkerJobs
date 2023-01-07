# What is Hosted Service?

  The hosted service is a one project that can to run a multiple actions in background using a delay time. We can use that to run for example: 
    - Create routines to execute any procedure, with web scrapping, feth data on database.
    - Send notifications from time to time.
    - Generate data sumary.

# Hosted Service documentation

  [Microsoft Documentation](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services)
  
# About this project

  Was created a hosted service to get a dolar price using a external API using a 1 second of delay. So, one by one second this service get a current dolar value and send to database in background. 
  
  This hosted service have too a second routine on this same hosted service: We created a routine to get a average dolar price and send this value to whatsapp message using a external API. And in this case, we used a 60 seconds to delay. So, 60 by 60 seconds this service send a message to whatsapp.
  
# How to use this hosted service

  You need to create a account on [Whatsapp Api](https://user.ultramsg.com), after that, please create a api instance on this api (the tutorial is there).
  
  After that, you need to create a database on your [environment](CreateDatabase.sql)
