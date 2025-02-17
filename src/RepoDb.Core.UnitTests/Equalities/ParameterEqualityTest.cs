﻿using System.Collections;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RepoDb.UnitTests.Equalities;

[TestClass]
public class ParameterEqualityTest
{
    [TestMethod]
    public void TestParameterEqualityFromString()
    {
        // Prepare
        var objA = new Parameter("ParameterName", new object());

        // Act
        var equal = Equals(objA.GetHashCode(), "ParameterName".GetHashCode());

        // Assert
        Assert.IsTrue(equal);
    }

    [TestMethod]
    public void TestParameterEqualityToImproperString()
    {
        // Prepare
        var objA = new Parameter("ParameterName", new object());

        // Act
        var equal = Equals(objA, "Parametername");

        // Assert
        Assert.IsFalse(equal);
    }

    [TestMethod]
    public void TestParameterHashCodeEquality()
    {
        // Prepare
        var objA = new Parameter("ParameterName", new object());
        var objB = new Parameter("ParameterName", new object());

        // Act
        var equal = (objA.GetHashCode() == objB.GetHashCode());

        // Assert
        Assert.IsTrue(equal);
    }

    [TestMethod]
    public void TestParameterHashCodeEqualityFromString()
    {
        // Prepare
        var objA = new Parameter("ParameterName", new object());

        // Act
        var equal = (objA.GetHashCode() == "ParameterName".GetHashCode());

        // Assert
        Assert.IsTrue(equal);
    }

    [TestMethod]
    public void TestParameterHashCodeEqualityFromImproperString()
    {
        // Prepare
        var objA = new Parameter("ParameterName", new object());

        // Act
        var equal = (objA.GetHashCode() == "Parametername".GetHashCode());

        // Assert
        Assert.IsFalse(equal);
    }

    [TestMethod]
    public void TestParameterObjectEquality()
    {
        // Prepare
        var objA = new Parameter("ParameterName", new object());
        var objB = new Parameter("ParameterName", new object());

        // Act
        var equal = (objA == objB);

        // Assert
        Assert.IsTrue(equal);
    }

    [TestMethod]
    public void TestParameterObjectEqualityFromEqualsMethod()
    {
        // Prepare
        var objA = new Parameter("ParameterName", new object());
        var objB = new Parameter("ParameterName", new object());

        // Act
        var equal = Equals(objA, objB);

        // Assert
        Assert.IsTrue(equal);
    }

    [TestMethod]
    public void TestParameterArrayListContainability()
    {
        // Prepare
        var objA = new Parameter("ParameterName", new object());
        var objB = new Parameter("ParameterName", new object());
        var list = new ArrayList();

        // Act
        list.Add(objA);
        var equal = list.Contains(objB);

        // Assert
        Assert.IsTrue(equal);
    }

    [TestMethod]
    public void TestParameterGenericListContainability()
    {
        // Prepare
        var objA = new Parameter("ParameterName", new object());
        var objB = new Parameter("ParameterName", new object());
        var list = new List<Parameter>();

        // Act
        list.Add(objA);
        var equal = list.Contains(objB);

        // Assert
        Assert.IsTrue(equal);
    }

    [TestMethod]
    public void TestParameterWithDbTypeHashCodeEquality()
    {
        // Prepare
        var objA = new Parameter("ParameterName", new object(), DbType.StringFixedLength);
        var objB = new Parameter("ParameterName", new object(), DbType.StringFixedLength);

        // Act
        var equal = (objA.GetHashCode() == objB.GetHashCode());

        // Assert
        Assert.IsTrue(equal);
    }

    [TestMethod]
    public void TestParameterWithDbTypeObjectEquality()
    {
        // Prepare
        var objA = new Parameter("ParameterName", new object(), DbType.StringFixedLength);
        var objB = new Parameter("ParameterName", new object(), DbType.StringFixedLength);

        // Act
        var equal = (objA == objB);

        // Assert
        Assert.IsTrue(equal);
    }
}
