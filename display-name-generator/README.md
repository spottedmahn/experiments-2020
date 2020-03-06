[Building a Camel Case @DisplayNameGenerator For JUnit 5](https://leeturner.me/posts/building-a-camel-case-junit5-displaynamegenerator/)

## VS Code Test Runner Output

![](readme-resources/2020-03-06-14-10-02.png)

## Code is being called 🤔🤷‍♂️

![](readme-resources/2020-03-06-14-10-52.png)

```
private String replaceCapitals(String name) {
  name = name.replaceAll("([A-Z])", " $1");
  name = name.replaceAll("([0-9]+)", " $1");
  System.out.println(String.format("new test name: '%s'", name));
  return name;
}
```