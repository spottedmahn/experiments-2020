package display.name.generator.util;

import java.lang.reflect.Method;

import org.junit.jupiter.api.DisplayNameGenerator;

//https://leeturner.me/posts/building-a-camel-case-junit5-displaynamegenerator/
public class ReplaceCamelCase extends DisplayNameGenerator.Standard {
  public ReplaceCamelCase() {
  }

  public String generateDisplayNameForClass(final Class<?> testClass) {
      return this.replaceCapitals(super.generateDisplayNameForClass(testClass));
  }

  public String generateDisplayNameForNestedClass(final Class<?> nestedClass) {
      return this.replaceCapitals(super.generateDisplayNameForNestedClass(nestedClass));
  }

  public String generateDisplayNameForMethod(final Class<?> testClass, final Method testMethod) {
    return this.replaceCapitals(testMethod.getName());
  }

  private String replaceCapitals(String name) {
    name = name.replaceAll("([A-Z])", " $1");
    name = name.replaceAll("([0-9]+)", " $1");
    System.out.println(String.format("new test name: '%s'", name));
    return name;
  }
}