using UnityEngine;
using System.Collections;
using System;

class GaussProgram {

	// 选主元
	public static void selectMainElement(int n, int k, double[,] a)
	{
		double t, mainElement;
		int l; 
		mainElement = Math.Abs(a[k, k]);

		l = k;
		for (int i = k; i < n; ++i) 
		{
			if (mainElement < Math.Abs (a[i, k])) 
			{
				mainElement = Math.Abs (a[i, k]);
				l = i;
			}
		}

		if (l != k) 
		{
			for (int j = k; j <= n; ++j) 
			{
				t = a [k, j];
				a[k, j] = a[l, j];
				a[l, j] = t;
			}
		}
	}

	/// <summary>
	/// Gauss 列主消元法求n元一次方程组的解
	/// </summary>
	/// <param name="n">传递过来的矩阵a的行数</param>
	/// <param name="a">方程组的增广矩阵</param>
	public static double[] Gauss(int n, double[,] a)
	{
		double[] x = new double[n];
		// 消元过程 最外层循环是找主元
		for (int k = 0; k < n - 1; ++k) 
		{
			selectMainElement (n, k, a);

			for (int i = k; i < n - 1; ++i) 
			{ 
				double m = a[i + 1, k] / a[k, k]; 
				for (int j = k; j <= n; ++j) 
				{
					a [i + 1, j] = a [i + 1, j] - m * a [k, j];
				}
			}
		}

		// 回代求解过程
		for (int k = n - 1; k >= 0; --k) 
		{
			double addResult = 0.0;
			for (int j = k + 1; j < n; ++j) 
			{
				addResult += x[j] * a[k, j];
			}
			x[k] = (a[k, n] - addResult) / a[k, k];
		}

		return x;

	}


}
