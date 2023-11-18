﻿using System.Reflection;
using NUnit.Framework;

namespace InStock.UnitTesting
{
  public abstract class TestBase
  {
    protected TestBase()
    {
      TodayUtc = DateTime.UtcNow.Date;

      YesterdayUtc = TodayUtc.AddDays(-1);

      TomorrowUtc = TodayUtc.AddDays(1);
    }

    protected DateTime YesterdayUtc { get; }

    protected DateTime TodayUtc { get; }

    protected DateTime TomorrowUtc { get; }

    //https://stackoverflow.com/a/9035421/603807
    /// <summary>
    ///   Get a private method handle from a class instance
    /// </summary>
    /// <typeparam name="T">Class of type T</typeparam>
    /// <param name="classRef">Instantiated reference of class type T</param>
    /// <param name="methodName">Name of private method inside of class</param>
    /// <returns>Method info handle</returns>
    protected MethodInfo GetPrivateMethod<T>(T classRef, string methodName)
    {
      var method = GetMethod(
        classRef,
        methodName,
        BindingFlags.NonPublic | BindingFlags.Instance);

      return method;
    }

    /// <summary>
    ///   Get a private static method handle from a static class
    /// </summary>
    /// <typeparam name="T">Static class of type T</typeparam>
    /// <param name="classRef">Reference of static class type T</param>
    /// <param name="methodName">Name of private static method inside of static class</param>
    /// <returns>Method info handle</returns>
    protected MethodInfo GetPrivateStaticMethod<T>(T classRef, string methodName)
    {
      var method = GetMethod(
        classRef,
        methodName,
        BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

      return method;
    }

    /// <summary>
    ///   Invoke a private method from a class instance
    /// </summary>
    /// <typeparam name="T">Class of type T</typeparam>
    /// <typeparam name="R">Return type of type R - use object if method's return type is void</typeparam>
    /// <param name="classRef">Instantiated reference of class type T</param>
    /// <param name="methodName">Name of private method inside of class</param>
    /// <param name="parameters">Parameters of method, pass null if parameter-less</param>
    /// <returns>Return result or null if void</returns>
    protected R InvokePrivateMethod<T, R>(T classRef, string methodName, params object[] parameters)
    {
      var method = GetPrivateMethod(classRef, methodName);

      var result = InvokeMethod<T, R>(classRef, method, parameters);

      return result;
    }

    protected void InvokePrivateMethod<T>(T classRef, string methodName, params object[] parameters)
    {
      var method = GetPrivateMethod(classRef, methodName);

      method.Invoke(classRef, parameters);
    }

    /// <summary>
    ///   Invoke a static private method from a static class instance
    /// </summary>
    /// <typeparam name="T">Static class of type T</typeparam>
    /// <typeparam name="R">Return type of type R - use object if method's return type is void</typeparam>
    /// <param name="classRef">Reference of static class type T</param>
    /// <param name="methodName">Name of private method inside of static class</param>
    /// <param name="parameters">Parameters of method, pass null if parameter-less</param>
    /// <returns>Return result or null if void</returns>
    protected R InvokePrivateStaticMethod<T, R>(T classRef, string methodName, params object[] parameters)
    {
      var method = GetPrivateStaticMethod(classRef, methodName);

      var result = InvokeMethod<T, R>(classRef, method, parameters);

      return result;
    }

    private static MethodInfo GetMethod<T>(T classRef, string methodName, BindingFlags flags)
    {
      if (string.IsNullOrWhiteSpace(methodName)) Assert.Fail("Method name cannot be null or whitespace");

      var method = classRef
        .GetType()
        .GetMethod(methodName, flags);

      if (method == null) Assert.Fail($"{methodName} method not found.");

      return method;
    }

    private static R InvokeMethod<T, R>(T classRef, MethodInfo method, params object[] parameters)
    {
      var obj = method.Invoke(classRef, parameters);

      var result = (R)obj;

      return result;
    }

    protected void AssertAreEqual(decimal expected, decimal actual, double delta = 0.01)
      => Assert.AreEqual((double)expected, (double)actual, delta);

    protected void AssertAreEqual<T>(T left, T right)
      where T : class, IEquatable<T>, new()
    {
      //Act
      var areEqual = Equals(left, right); // left.Equals(right) and == won't work here

      //Assert
      Assert.IsTrue(areEqual);
    }

    protected void AssertAreNotEqual<T>(T left, T right)
      where T : class, IEquatable<T>, new()
    {
      //Act
      var areEqual = Equals(left, right); // left.Equals(right) and == won't work here

      //Assert
      Assert.IsFalse(areEqual);
    }

    protected IList<T> AsList<T>(T element)
      => new List<T> { element };
  }
}
