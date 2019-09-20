# Akinator API for .NET Standard 2.0

This is an improved async version of the Akinator API, written for .NET Standard 2.0

[![Build status master](https://ci.appveyor.com/api/projects/status/al1pxhaokdi8ymqe?svg=true&passingText=master%20-%20passing&failingText=master%20-%20failing&pendingText=master%20-%20pending)](https://ci.appveyor.com/project/janniksam/Akinator-Api-Net) 
[![NuGet version](https://badge.fury.io/nu/Akinator.Api.Net.svg)](https://badge.fury.io/nu/Akinator.Api.Net)

- Inspired by: https://github.com/davidsl4/AkiNet (not working anymore)
- Ported from: https://github.com/jgoralcz/aki-api/ (NodeJS implementation)

## Supported servers

This is a WIP project and in a very early stage. Currently only some servers are supported:

| Language | Servers |
| --- | --- |
| Arabic | Person |
| English | Animal, Object, Person |
| French | Animal, Object, Person |
| German | Person, Animal |
| Italian | Animal, Person |
| Russian | Person |
| Spanish | Animal, Person |

All the other servers are coming later or if someone requests it. They are not hard to add anyways, so feel free to make a pull request.

## Known issues

- Not all servers are currently supported.
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
