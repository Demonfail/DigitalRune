﻿using System;
using NUnit.Framework;


namespace DigitalRune.Mathematics.Algebra.Tests
{
  [TestFixture]
  public class JacobiMethodFTest
  {
    [Test]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void MaxNumberOfIterationsException()
    {
      new JacobiMethodF().MaxNumberOfIterations = -1;
    }


    [Test]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void EpsilonException()
    {
      new JacobiMethodF().Epsilon = -0.001f;
    }


    [Test]
    public void SolveWithDefaultInitialGuess()
    {
      MatrixF A = new MatrixF(new float[,] { { 4 } });
      VectorF b = new VectorF(new float[] { 20 });

      JacobiMethodF solver = new JacobiMethodF();
      VectorF x = solver.Solve(A, b);

      Assert.IsTrue(VectorF.AreNumericallyEqual(new VectorF(1, 5), x));
      Assert.AreEqual(2, solver.NumberOfIterations);
    }



    [Test]
    public void Test1()
    {
      MatrixF A = new MatrixF(new float[,] { { 4 } });
      VectorF b = new VectorF(new float[] { 20 });

      JacobiMethodF solver = new JacobiMethodF();
      VectorF x = solver.Solve(A, null, b);

      Assert.IsTrue(VectorF.AreNumericallyEqual(new VectorF(1, 5), x));
      Assert.AreEqual(2, solver.NumberOfIterations);
    }


    [Test]
    public void Test2()
    {
      MatrixF A = new MatrixF(new float[,] { { 1, 0 }, 
                                             { 0, 1 }});
      VectorF b = new VectorF(new float[] { 20, 28 });

      JacobiMethodF solver = new JacobiMethodF();
      VectorF x = solver.Solve(A, null, b);

      Assert.IsTrue(VectorF.AreNumericallyEqual(b, x));
      Assert.AreEqual(2, solver.NumberOfIterations);
    }


    [Test]
    public void Test3()
    {
      MatrixF A = new MatrixF(new float[,] { { 2, 0 }, 
                                             { 0, 2 }});
      VectorF b = new VectorF(new float[] { 20, 28 });

      JacobiMethodF solver = new JacobiMethodF();
      VectorF x = solver.Solve(A, null, b);

      Assert.IsTrue(VectorF.AreNumericallyEqual(b/2, x));
      Assert.AreEqual(2, solver.NumberOfIterations);
    }


    [Test]
    public void Test4()
    {
      MatrixF A = new MatrixF(new float[,] { { -12, 2 }, 
                                             { 2, 3 }});
      VectorF b = new VectorF(new float[] { 20, 28 });

      JacobiMethodF solver = new JacobiMethodF();
      VectorF x = solver.Solve(A, null, b);

      VectorF solution = MatrixF.SolveLinearEquations(A, b);
      Assert.IsTrue(VectorF.AreNumericallyEqual(solution, x));      
    }


    [Test]
    public void Test5()
    {
      MatrixF A = new MatrixF(new float[,] { { -21, 2, -4, 0 }, 
                                             { 2, 3, 0.1f, -1 },
                                             { 2, 10, 111.1f, -11 },
                                             { 23, 112, 111.1f, -143 }});
      VectorF b = new VectorF(new float[] { 20, 28, -12, 0.1f });

      JacobiMethodF solver = new JacobiMethodF();
      VectorF x = solver.Solve(A, null, b);

      VectorF solution = MatrixF.SolveLinearEquations(A, b);
      Assert.IsTrue(VectorF.AreNumericallyEqual(solution, x));
    }


    [Test]
    public void Test6()
    {
      MatrixF A = new MatrixF(new float[,] { { -21, 2, -4, 0 }, 
                                             { 2, 3, 0.1f, -1 },
                                             { 2, 10, 111.1f, -11 },
                                             { 23, 112, 111.1f, -143 }});
      VectorF b = new VectorF(new float[] { 20, 28, -12, 0.1f });

      JacobiMethodF solver = new JacobiMethodF();
      solver.MaxNumberOfIterations = 10;
      VectorF x = solver.Solve(A, null, b);

      VectorF solution = MatrixF.SolveLinearEquations(A, b);
      Assert.IsFalse(VectorF.AreNumericallyEqual(solution, x));
      Assert.AreEqual(10, solver.NumberOfIterations);
    }


    [Test]
    public void Test7()
    {
      MatrixF A = new MatrixF(new float[,] { { -21, 2, -4, 0 }, 
                                             { 2, 3, 0.1f, -1 },
                                             { 2, 10, 111.1f, -11 },
                                             { 23, 112, 111.1f, -143 }});
      VectorF b = new VectorF(new float[] { 20, 28, -12, 0.1f });

      JacobiMethodF solver = new JacobiMethodF();
      solver.MaxNumberOfIterations = 10;
      solver.Epsilon = 0.1f;
      VectorF x = solver.Solve(A, null, b);

      VectorF solution = MatrixF.SolveLinearEquations(A, b);
      Assert.IsTrue(VectorF.AreNumericallyEqual(solution, x, 0.1f));
      Assert.IsFalse(VectorF.AreNumericallyEqual(solution, x));
      Assert.Greater(10, solver.NumberOfIterations);
    }


    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestArgumentNullException()
    {
      JacobiMethodF solver = new JacobiMethodF();
      VectorF x = solver.Solve(null, null, new VectorF());
    }


    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestArgumentNullException2()
    {
      JacobiMethodF solver = new JacobiMethodF();
      VectorF x = solver.Solve(new MatrixF(), null, null);
    }


    [Test]
    [ExpectedException(typeof(ArgumentException))]
    public void TestArgumentException()
    {
      JacobiMethodF solver = new JacobiMethodF();
      VectorF x = solver.Solve(new MatrixF(3, 4), null, new VectorF(3));
    }


    [Test]
    [ExpectedException(typeof(ArgumentException))]
    public void TestArgumentException2()
    {
      JacobiMethodF solver = new JacobiMethodF();
      VectorF x = solver.Solve(new MatrixF(3, 3), null, new VectorF(4));
    }


    [Test]
    [ExpectedException(typeof(ArgumentException))]
    public void TestArgumentException3()
    {
      JacobiMethodF solver = new JacobiMethodF();
      VectorF x = solver.Solve(new MatrixF(3, 3), new VectorF(4), new VectorF(3));
    }
  }
}
