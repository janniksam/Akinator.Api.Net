# Akinator API for .NET Standard 1.4

This is an improved async version of the Akinator Api, written for .NET Standard 1.4

[![Build status master](https://ci.appveyor.com/api/projects/status/al1pxhaokdi8ymqe?svg=true&passingText=master%20-%20passing&failingText=master%20-%20failing&pendingText=master%20-%20pending)](https://ci.appveyor.com/project/janniksam/Akinator-Api-Net) 
[![NuGet version](https://badge.fury.io/nu/Akinator.Api.Net.svg)](https://badge.fury.io/nu/Akinator.Api.Net)

- Inspired by: https://github.com/davidsl4/AkiNet (not working anymore)
- Ported from: https://github.com/jgoralcz/aki-api/ (NodeJS implementation)

## Languages

As of April 2020, 16 languages are supported by the Api-Client:

- Arabic
- Chinese
- Dutch
- English
- French
- German
- Indonesian
- Israeli
- Italian
- Japanese
- Korean
- Polski
- Portuguese
- Russian
- Spanish
- Turkish

Further more there are five different server types to choose from. Not every language does support every server type:

- Person
- Animal
- Object
- Movie
- Place

Other languages and server-types will be added, as soon as Akinator is starting to support them.

## Basic usage

```cs
// We will search for a german person server to play on.
// The initial search will take a few seconds, because it loads every server-url and 
// performs a health check for each possible server combination it can find.
var serverLocator = new AkinatorServerLocator(); 
var server = await serverLocator.SearchAsync(Language.German, ServerType.Person);

// Opening
using (var client = new AkinatorClient(server))
{
   // Start a new game
   var question = await client.StartNewGame(); 
   
   // Process question...
   // ...
   
   // Answer the previous question with "Yes" and get the next one
   var question = await client.Answer(AnswerOptions.Yes);
   
   // if Akinator is due to guess...
   if (client.GuessIsDue())
   {
      // Get Akinators guess..
      var guess = await client.GetGuess();
      // Verify the guess ...
   }
}
```
