# Akinator Api for .NET Core

[![Build status master](https://ci.appveyor.com/api/projects/status/al1pxhaokdi8ymqe?svg=true&passingText=master%20-%20passing&failingText=master%20-%20failing&pendingText=master%20-%20pending)](https://ci.appveyor.com/project/janniksam/Akinator.Api.Net) 
[![NuGet version](https://badge.fury.io/nu/Akinator.Api.Net.svg)](https://badge.fury.io/nu/Akinator.Api.Net)

- Inspired by: https://github.com/davidsl4/AkiNet (not working anymore)
- Ported from: https://github.com/jgoralcz/aki-api/ (NodeJS implementation)

## WIP

This is a WIP project and in a very early stage. Currently only some servers are supported:

- the german person & animal servers
- the english person server

All the other servers are coming later or if someone requests it. They are not hard to add anyways.

## Known issues

- Lack of servers
- The GuessIsDue-method, which decides, when Akinator is ready to guess, needs to be improved significantly

## Basic usage

```cs
using (var client = new AkinatorClient(Language.German, ServerType.Person))
{
   // Start a new game
   var question = await client.StartNewGame(); 
   
   // Process question...
   // ...
   
   // Answer the previous question with "Yes" and get the next one
   var question = await client.Answer(AnswerOptions.Yes);
   
   // if Akinator is due to guess...
   if (client.GuessIsDue(question))
   {
      // Get Akinators guess..
      var guess = await client.GetGuess();
      // Verify the guess ...
   }
}
```
