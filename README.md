# Pluto Rover 

The solution contains two .NET 5 class libraries and a unit test project.

## Design decisions
- Tried to strike a balance between refactoring and complexity
- I used MediatR to have a simple command and a handler.
- I liked the idea of queueing movements and passed them as part of the command.
- The command handler emits domain events, these can be routed to other parts of the Rover software.
- This shows a basic DDD pattern. 

## How to run:
* Clone repository from: https://github.com/nickpts/Pluto.Rover.git
* You will need to have .NET 5 installed. 
* Enter Pluto.Rover.Tests directory.
* Type dotnet test. 
* Tests should pass.

## Todo:
* A rover controller class wrapping command and handler classes

## Feedback:
* In the assessment did you easily understand what was asked of you? (yes)
* In the assessment did you feel 2 hours was sufficient in completing the task? (Probably, to do the bare minimum. I took longer.)
* Given more time was there any particular area you thought you could improve? (Maybe unit tests with more complicated routes)
* Do you have any thoughts or feedback you would like to provide for this assessment? (I found it enjoyable)
