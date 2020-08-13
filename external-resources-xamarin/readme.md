## Problem desc
In a [Xamarin Forms](https://github.com/xamarin/Xamarin.Forms) app, I want to have separate "style" files; I don't want a huge `App.xaml` with all my shared styles/colors/etc.  I also want to share styles to handle light/dark and I don't want to violate [DRY](https://en.wikipedia.org/wiki/Don%27t_repeat_yourself).

### References
[Xamarin Forums: External ResourceDictionary in several projects](https://forums.xamarin.com/discussion/174030/external-resourcedictionary-in-several-projects)

[Better Resource Organization in Xamarin.Forms](https://devblogs.microsoft.com/xamarin/better-resource-organization-xamarin-forms/)

[App Themes for Xamarin.Forms](https://devblogs.microsoft.com/xamarin/app-themes-xamarin-forms/)

[Stack Overflow Question trying to solve the same problem](https://stackoverflow.com/questions/58137147/xamarin-forms-merge-resources)

## Final result
### Light mode
![](readme-resources/2020-08-13-10-46-16.png)

### Dark mode
![](readme-resources/2020-08-13-10-46-57.png)

## Key details

- You can't use the new resources approach.*
- Each separate resource file **NEEDS** to have a code behind file w/ a call to `InitializeComponent()`

\* You can use parts of it but not all of it.  You can reference files via the `Source` property but you can't eliminate `<ResourceDictionary>` in *App.xaml* like [described here](https://devblogs.microsoft.com/xamarin/better-resource-organization-xamarin-forms/#upcoming-resourcedictionary-improvements):
> You can simple write

## Important Files

### App.xaml
![](readme-resources/2020-08-13-10-49-09.png)

### Colors.xaml
![](readme-resources/2020-08-13-10-49-56.png)

### Colors.xaml.cs
![](readme-resources/2020-08-13-10-50-48.png)

## Light/Dark mode
### BaseStyles.xaml
![](readme-resources/2020-08-13-11-05-19.png)

## Utilization

### ListPage.xaml
![](readme-resources/2020-08-13-11-06-42.png)

## Debugging Tips

**Protip**: In VS for Mac you can add an Exception Checkpoint so that VS will break when there's a problem otherwise they are swallowed it just doesn't work.

### "Break on exceptions"

![](readme-resources/2020-08-13-12-20-05.png)

### Example

![](readme-resources/2020-08-13-12-21-24.png)