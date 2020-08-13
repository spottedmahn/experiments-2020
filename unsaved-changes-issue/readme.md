## Problem Desc
If I have two unsaved changes and I start up the debugger, those changes are not reflected in my debug session.

My suspicion is something isn't waiting for everything to finish before doing it's thing of copying over the to the simulator.

Maybe the code generation for the C# code of the XAML?

## Screen recoring
[screen recording MP4](readme-resources/Unsaved%20changes%20bug%202020-08-12%20at%2021.48.08.mp4)

## Screen recording breakdown

1. I show the app working in the debugger
1. Stop the debugger
1. Make two file changes
1. Start the debugger **W/O** saving the files manually
1. The app still appears to work but it really doesn't
1. Stop the debugger and restart it
1. The app is now broken
1. If I had manually saved the files it would have broken on the first try
