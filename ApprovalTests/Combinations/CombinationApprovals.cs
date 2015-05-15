using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ApprovalTests.Combinations
{
	public class CombinationApprovals
	{
		private static readonly object[] EMPTY = { null };

		public static void VerifyAllCombinations<A>(Func<A, object> processCall, IEnumerable<A> aList)
		{
			VerifyAllCombinations((a, b, c, d, e, f, g, h, i) => processCall(a), "[{0}]", aList, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY);
		}

		public static void VerifyAllCombinations<A>(Func<A, object> processCall, Func<object, string> resultFormatter, IEnumerable<A> aList)
		{
			VerifyAllCombinations((a, b, c, d, e, f, g, h, i) => processCall(a), "[{0}]", resultFormatter, aList, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY);
		}

		public static void VerifyAllCombinations<A, B>(Func<A, B, object> processCall, IEnumerable<A> aList, IEnumerable<B> bList)
		{
			VerifyAllCombinations((a, b, c, d, e, f, g, h, i) => processCall(a, b), "[{0},{1}]", aList, bList, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY);
		}

		public static void VerifyAllCombinations<A, B>(Func<A, B, object> processCall, Func<object, string> resultFormatter, IEnumerable<A> aList, IEnumerable<B> bList)
		{
			VerifyAllCombinations((a, b, c, d, e, f, g, h, i) => processCall(a, b), "[{0},{1}]", resultFormatter, aList, bList, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY);
		}

		public static void VerifyAllCombinations<A, B, C>(Func<A, B, C, object> processCall, IEnumerable<A> aList, IEnumerable<B> bList, IEnumerable<C> cList)
		{
			VerifyAllCombinations((a, b, c, d, e, f, g, h, i) => processCall(a, b, c), "[{0},{1},{2}]", aList, bList, cList, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY);
		}

		public static void VerifyAllCombinations<A, B, C>(Func<A, B, C, object> processCall, Func<object, string> resultFormatter, IEnumerable<A> aList, IEnumerable<B> bList, IEnumerable<C> cList)
		{
			VerifyAllCombinations((a, b, c, d, e, f, g, h, i) => processCall(a, b, c), "[{0},{1},{2}]", resultFormatter, aList, bList, cList, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY);
		}

		public static void VerifyAllCombinations<A, B, C, D>(Func<A, B, C, D, object> processCall, IEnumerable<A> aList, IEnumerable<B> bList, IEnumerable<C> cList, IEnumerable<D> dList)
		{
			VerifyAllCombinations((a, b, c, d, e, f, g, h, i) => processCall(a, b, c, d), "[{0},{1},{2},{3}]", aList, bList, cList, dList, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY);
		}

		public static void VerifyAllCombinations<A, B, C, D>(Func<A, B, C, D, object> processCall, Func<object, string> resultFormatter, IEnumerable<A> aList, IEnumerable<B> bList, IEnumerable<C> cList, IEnumerable<D> dList)
		{
			VerifyAllCombinations((a, b, c, d, e, f, g, h, i) => processCall(a, b, c, d), "[{0},{1},{2},{3}]", resultFormatter, aList, bList, cList, dList, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY);
		}

		public static void VerifyAllCombinations<A, B, C, D, E>(Func<A, B, C, D, E, object> processCall, IEnumerable<A> aList, IEnumerable<B> bList, IEnumerable<C> cList, IEnumerable<D> dList, IEnumerable<E> eList)
		{
			VerifyAllCombinations((a, b, c, d, e, f, g, h, i) => processCall(a, b, c, d, e), "[{0},{1},{2},{3},{4}]", aList, bList, cList, dList, eList, EMPTY, EMPTY, EMPTY, EMPTY);
		}

		public static void VerifyAllCombinations<A, B, C, D, E>(Func<A, B, C, D, E, object> processCall, Func<object, string> resultFormatter, IEnumerable<A> aList, IEnumerable<B> bList, IEnumerable<C> cList, IEnumerable<D> dList, IEnumerable<E> eList)
		{
			VerifyAllCombinations((a, b, c, d, e, f, g, h, i) => processCall(a, b, c, d, e), "[{0},{1},{2},{3},{4}]", resultFormatter, aList, bList, cList, dList, eList, EMPTY, EMPTY, EMPTY, EMPTY);
		}

		public static void VerifyAllCombinations<A, B, C, D, E, F>(Func<A, B, C, D, E, F, object> processCall, IEnumerable<A> aList, IEnumerable<B> bList, IEnumerable<C> cList, IEnumerable<D> dList, IEnumerable<E> eList, IEnumerable<F> fList)
		{
			VerifyAllCombinations((a, b, c, d, e, f, g, h, i) => processCall(a, b, c, d, e, f), "[{0},{1},{2},{3},{4},{5}]", aList, bList, cList, dList, eList, fList, EMPTY, EMPTY, EMPTY);
		}

		public static void VerifyAllCombinations<A, B, C, D, E, F>(Func<A, B, C, D, E, F, object> processCall, Func<object, string> resultFormatter, IEnumerable<A> aList, IEnumerable<B> bList, IEnumerable<C> cList, IEnumerable<D> dList, IEnumerable<E> eList, IEnumerable<F> fList)
		{
			VerifyAllCombinations((a, b, c, d, e, f, g, h, i) => processCall(a, b, c, d, e, f), "[{0},{1},{2},{3},{4},{5}]", resultFormatter, aList, bList, cList, dList, eList, fList, EMPTY, EMPTY, EMPTY);
		}

		public static void VerifyAllCombinations<A, B, C, D, E, F, G>(Func<A, B, C, D, E, F, G, object> processCall, IEnumerable<A> aList, IEnumerable<B> bList, IEnumerable<C> cList, IEnumerable<D> dList, IEnumerable<E> eList, IEnumerable<F> fList, IEnumerable<G> gList)
		{
			VerifyAllCombinations((a, b, c, d, e, f, g, h, i) => processCall(a, b, c, d, e, f, g), "[{0},{1},{2},{3},{4},{5},{6}]", aList, bList, cList, dList, eList, fList, gList, EMPTY, EMPTY);
		}

		public static void VerifyAllCombinations<A, B, C, D, E, F, G>(Func<A, B, C, D, E, F, G, object> processCall, Func<object, string> resultFormatter, IEnumerable<A> aList, IEnumerable<B> bList, IEnumerable<C> cList, IEnumerable<D> dList, IEnumerable<E> eList, IEnumerable<F> fList, IEnumerable<G> gList)
		{
			VerifyAllCombinations((a, b, c, d, e, f, g, h, i) => processCall(a, b, c, d, e, f, g), "[{0},{1},{2},{3},{4},{5},{6}]", resultFormatter, aList, bList, cList, dList, eList, fList, gList, EMPTY, EMPTY);
		}

		public static void VerifyAllCombinations<A, B, C, D, E, F, G, H>(Func<A, B, C, D, E, F, G, H, object> processCall, IEnumerable<A> aList, IEnumerable<B> bList, IEnumerable<C> cList, IEnumerable<D> dList, IEnumerable<E> eList, IEnumerable<F> fList, IEnumerable<G> gList, IEnumerable<H> hList)
		{
			VerifyAllCombinations((a, b, c, d, e, f, g, h, i) => processCall(a, b, c, d, e, f, g, h), "[{0},{1},{2},{3},{4},{5},{6},{7}]", aList, bList, cList, dList, eList, fList, gList, hList, EMPTY);
		}

		public static void VerifyAllCombinations<A, B, C, D, E, F, G, H>(Func<A, B, C, D, E, F, G, H, object> processCall, Func<object, string> resultFormatter, IEnumerable<A> aList, IEnumerable<B> bList, IEnumerable<C> cList, IEnumerable<D> dList, IEnumerable<E> eList, IEnumerable<F> fList, IEnumerable<G> gList, IEnumerable<H> hList)
		{
			VerifyAllCombinations((a, b, c, d, e, f, g, h, i) => processCall(a, b, c, d, e, f, g, h), "[{0},{1},{2},{3},{4},{5},{6},{7}]", resultFormatter, aList, bList, cList, dList, eList, fList, gList, hList, EMPTY);
		}

		public static void VerifyAllCombinations<A, B, C, D, E, F, G, H, I>(Func<A, B, C, D, E, F, G, H, I, object> processCall, IEnumerable<A> aList, IEnumerable<B> bList, IEnumerable<C> cList, IEnumerable<D> dList, IEnumerable<E> eList, IEnumerable<F> fList, IEnumerable<G> gList, IEnumerable<H> hList, IEnumerable<I> iList)
		{
			VerifyAllCombinations(processCall, "[{0},{1},{2},{3},{4},{5},{6},{7},{8}]", aList, bList, cList, dList, eList, fList, gList, hList, iList);
		}

		public static void VerifyAllCombinations<A, B, C, D, E, F, G, H, I>(Func<A, B, C, D, E, F, G, H, I, object> processCall, Func<object, string> resultFormatter, IEnumerable<A> aList, IEnumerable<B> bList, IEnumerable<C> cList, IEnumerable<D> dList, IEnumerable<E> eList, IEnumerable<F> fList, IEnumerable<G> gList, IEnumerable<H> hList, IEnumerable<I> iList)
		{
			VerifyAllCombinations(processCall, "[{0},{1},{2},{3},{4},{5},{6},{7},{8}]", resultFormatter, aList, bList, cList, dList, eList, fList, gList, hList, iList);
		}

		private static void VerifyAllCombinations<A, B, C, D, E, F, G, H, I>(Func<A, B, C, D, E, F, G, H, I, object> processCall, string format, IEnumerable<A> aList, IEnumerable<B> bList, IEnumerable<C> cList, IEnumerable<D> dList, IEnumerable<E> eList, IEnumerable<F> fList, IEnumerable<G> gList, IEnumerable<H> hList, IEnumerable<I> iList)
		{
			VerifyAllCombinations(processCall, format, (result) => result + string.Empty, aList, bList, cList, dList, eList, fList, gList, hList, iList);
		}

		private static void VerifyAllCombinations<A, B, C, D, E, F, G, H, I>(Func<A, B, C, D, E, F, G, H, I, object> processCall, string format, Func<object, string> resultFormatter, IEnumerable<A> aList, IEnumerable<B> bList, IEnumerable<C> cList, IEnumerable<D> dList, IEnumerable<E> eList, IEnumerable<F> fList, IEnumerable<G> gList, IEnumerable<H> hList, IEnumerable<I> iList)
		{
			ApprovalTests.Approvals.Verify(GetApprovalString(processCall, format, resultFormatter, aList, bList, cList, dList, eList, fList, gList, hList, iList));
		}

		public static string GetApprovalString<A>(Func<A, object> processCall, string format, Func<object, string> resultFormatter,
																							IEnumerable<A> aList)
		{
			return GetApprovalString((a, b, c, d, e, f, g, h, i) => processCall(a), format, resultFormatter, aList, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY);
		}

		public static string GetApprovalString<A>(Func<A, object> processCall, IEnumerable<A> aList)
		{
			return GetApprovalString((a, b, c, d, e, f, g, h, i) => processCall(a), "[{0}]", (result) => result + string.Empty, aList, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY);
		}

		public static string GetApprovalString<A, B>(Func<A, B, object> processCall, string format, Func<object, string> resultFormatter,
																								 IEnumerable<A> aList, IEnumerable<B> bList)
		{
			return GetApprovalString((a, b, c, d, e, f, g, h, i) => processCall(a, b), format, resultFormatter, aList, bList, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY);
		}

		public static string GetApprovalString<A, B>(Func<A, B, object> processCall, IEnumerable<A> aList, IEnumerable<B> bList)
		{
			return GetApprovalString((a, b, c, d, e, f, g, h, i) => processCall(a, b), "[{0},{1}]", (result) => result + string.Empty, aList, bList, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY);
		}

		public static string GetApprovalString<A, B, C>(Func<A, B, C, object> processCall, string format, Func<object, string> resultFormatter,
																										IEnumerable<A> aList, IEnumerable<B> bList,
																										IEnumerable<C> cList)
		{
			return GetApprovalString((a, b, c, d, e, f, g, h, i) => processCall(a, b, c), format, resultFormatter, aList, bList, cList, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY);
		}

		public static string GetApprovalString<A, B, C>(Func<A, B, C, object> processCall, IEnumerable<A> aList, IEnumerable<B> bList, IEnumerable<C> cList)
		{
			return GetApprovalString((a, b, c, d, e, f, g, h, i) => processCall(a, b, c), "[{0},{1},{2}]", (result) => result + string.Empty, aList, bList, cList, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY);
		}

		public static string GetApprovalString<A, B, C, D>(Func<A, B, C, D, object> processCall, string format, Func<object, string> resultFormatter,
																											 IEnumerable<A> aList, IEnumerable<B> bList,
																											 IEnumerable<C> cList, IEnumerable<D> dList)
		{
			return GetApprovalString((a, b, c, d, e, f, g, h, i) => processCall(a, b, c, d), format, resultFormatter, aList, bList, cList, dList, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY);
		}

		public static string GetApprovalString<A, B, C, D>(Func<A, B, C, D, object> processCall, IEnumerable<A> aList, IEnumerable<B> bList,
																											 IEnumerable<C> cList, IEnumerable<D> dList)
		{
			return GetApprovalString((a, b, c, d, e, f, g, h, i) => processCall(a, b, c, d), "[{0},{1},{2},{3}]", (result) => result + string.Empty, aList, bList, cList, dList, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY);
		}

		public static string GetApprovalString<A, B, C, D, E>(Func<A, B, C, D, E, object> processCall, string format, Func<object, string> resultFormatter,
																													IEnumerable<A> aList, IEnumerable<B> bList,
																													IEnumerable<C> cList, IEnumerable<D> dList,
																													IEnumerable<E> eList)
		{
			return GetApprovalString((a, b, c, d, e, f, g, h, i) => processCall(a, b, c, d, e), format, resultFormatter, aList, bList, cList, dList, eList, EMPTY, EMPTY, EMPTY, EMPTY);
		}

		public static string GetApprovalString<A, B, C, D, E>(Func<A, B, C, D, E, object> processCall, IEnumerable<A> aList, IEnumerable<B> bList,
																													IEnumerable<C> cList, IEnumerable<D> dList,
																													IEnumerable<E> eList)
		{
			return GetApprovalString((a, b, c, d, e, f, g, h, i) => processCall(a, b, c, d, e), "[{0},{1},{2},{3},{4}]", (result) => result + string.Empty, aList, bList, cList, dList, eList, EMPTY, EMPTY, EMPTY, EMPTY);
		}

		public static string GetApprovalString<A, B, C, D, E, F>(Func<A, B, C, D, E, F, object> processCall, string format, Func<object, string> resultFormatter,
																														 IEnumerable<A> aList, IEnumerable<B> bList,
																														 IEnumerable<C> cList, IEnumerable<D> dList,
																														 IEnumerable<E> eList, IEnumerable<F> fList)
		{
			return GetApprovalString((a, b, c, d, e, f, g, h, i) => processCall(a, b, c, d, e, f), format, resultFormatter, aList, bList, cList, dList, eList, fList, EMPTY, EMPTY, EMPTY);
		}

		public static string GetApprovalString<A, B, C, D, E, F>(Func<A, B, C, D, E, F, object> processCall, IEnumerable<A> aList, IEnumerable<B> bList,
																														 IEnumerable<C> cList, IEnumerable<D> dList,
																														 IEnumerable<E> eList, IEnumerable<F> fList)
		{
			return GetApprovalString((a, b, c, d, e, f, g, h, i) => processCall(a, b, c, d, e, f), "[{0},{1},{2},{3},{4},{5}]", (result) => result + string.Empty, aList, bList, cList, dList, eList, fList, EMPTY, EMPTY, EMPTY);
		}

		public static string GetApprovalString<A, B, C, D, E, F, G>(Func<A, B, C, D, E, F, G, object> processCall, string format, Func<object, string> resultFormatter,
																																IEnumerable<A> aList, IEnumerable<B> bList,
																																IEnumerable<C> cList, IEnumerable<D> dList,
																																IEnumerable<E> eList, IEnumerable<F> fList,
																																IEnumerable<G> gList)
		{
			return GetApprovalString((a, b, c, d, e, f, g, h, i) => processCall(a, b, c, d, e, f, g), format, resultFormatter, aList, bList, cList, dList, eList, fList, gList, EMPTY, EMPTY);
		}

		public static string GetApprovalString<A, B, C, D, E, F, G>(Func<A, B, C, D, E, F, G, object> processCall, IEnumerable<A> aList, IEnumerable<B> bList,
																																IEnumerable<C> cList, IEnumerable<D> dList,
																																IEnumerable<E> eList, IEnumerable<F> fList,
																																IEnumerable<G> gList)
		{
			return GetApprovalString((a, b, c, d, e, f, g, h, i) => processCall(a, b, c, d, e, f, g), "[{0},{1},{2},{3},{4},{5},{6}]", (result) => result + string.Empty, aList, bList, cList, dList, eList, fList, gList, EMPTY, EMPTY);
		}

		public static string GetApprovalString<A, B, C, D, E, F, G, H>(Func<A, B, C, D, E, F, G, H, object> processCall, string format, Func<object, string> resultFormatter,
																																	 IEnumerable<A> aList, IEnumerable<B> bList,
																																	 IEnumerable<C> cList, IEnumerable<D> dList,
																																	 IEnumerable<E> eList, IEnumerable<F> fList,
																																	 IEnumerable<G> gList, IEnumerable<H> hList)
		{
			return GetApprovalString((a, b, c, d, e, f, g, h, i) => processCall(a, b, c, d, e, f, g, h), format, resultFormatter, aList, bList, cList, dList, eList, fList, gList, hList, EMPTY);
		}

		public static string GetApprovalString<A, B, C, D, E, F, G, H>(Func<A, B, C, D, E, F, G, H, object> processCall, IEnumerable<A> aList, IEnumerable<B> bList,
																																	 IEnumerable<C> cList, IEnumerable<D> dList,
																																	 IEnumerable<E> eList, IEnumerable<F> fList,
																																	 IEnumerable<G> gList, IEnumerable<H> hList)
		{
			return GetApprovalString((a, b, c, d, e, f, g, h, i) => processCall(a, b, c, d, e, f, g, h), "[{0},{1},{2},{3},{4},{5},{6},{7}]", (result) => result + string.Empty, aList, bList, cList, dList, eList, fList, gList, hList, EMPTY);
		}

		public static string GetApprovalString<A, B, C, D, E, F, G, H, I>(Func<A, B, C, D, E, F, G, H, I, object> processCall, IEnumerable<A> aList, IEnumerable<B> bList,
																																			IEnumerable<C> cList, IEnumerable<D> dList,
																																			IEnumerable<E> eList, IEnumerable<F> fList,
																																			IEnumerable<G> gList, IEnumerable<H> hList,
																																			IEnumerable<I> iList)
		{
			return GetApprovalString(processCall, "[{0},{1},{2},{3},{4},{5},{6},{7},{8}]", (result) => result + string.Empty, aList, bList, cList, dList, eList, fList, gList, hList, iList);
		}

		public static string GetApprovalString<A, B, C, D, E, F, G, H, I>(Func<A, B, C, D, E, F, G, H, I, object> processCall, string format, Func<object, string> resultFormatter,
																																			IEnumerable<A> aList, IEnumerable<B> bList,
																																			IEnumerable<C> cList, IEnumerable<D> dList,
																																			IEnumerable<E> eList, IEnumerable<F> fList,
																																			IEnumerable<G> gList, IEnumerable<H> hList,
																																			IEnumerable<I> iList)
		{
			var sb = new StringBuilder();
			AllCombinations((a, b, c, d, e, f, g, h, i) =>
												{
													object result;
													try
													{
														result = processCall(a, b, c, d, e, f, g, h, i);
													}
													catch (Exception ex)
													{
														result = ex.Message;
													}
													var input = String.Format(CultureInfo.InvariantCulture, format, a, b, c, d, e, f, g, h, i);
													sb.Append(String.Format("{0} => {1}\r\n", input, resultFormatter(result)));
												}, aList, bList, cList, dList, eList, fList, gList, hList, iList);

			var combinations = sb.ToString();
			return combinations;
		}


		private static void AllCombinations<A, B, C, D, E, F, G, H, I>(Action<A, B, C, D, E, F, G, H, I> processCall, IEnumerable<A> aList, IEnumerable<B> bList, IEnumerable<C> cList, IEnumerable<D> dList, IEnumerable<E> eList, IEnumerable<F> fList, IEnumerable<G> gList, IEnumerable<H> hList, IEnumerable<I> iList)
		{
			foreach (var a in aList)
				foreach (var b in bList)
					foreach (var c in cList)
						foreach (var d in dList)
							foreach (var e in eList)
								foreach (var f in fList)
									foreach (var g in gList)
										foreach (var h in hList)
											foreach (var i in iList)
											{
												processCall(a, b, c, d, e, f, g, h, i);
											}
		}
	}
}