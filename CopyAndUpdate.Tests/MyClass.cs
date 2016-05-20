﻿

using System;
using NUnit.Framework;

namespace CopyAndUpdate.Tests
{
	public class Model : ICloneable {
		public string A { set; get; }
		public string B { set; get; }
		public object Clone () {
			return this.MemberwiseClone ();
		}
	}

	public static class Extensions {
		public static T With<T> (this T data, Action<T> md) where T : ICloneable {
			var newData = (T)data.Clone ();
			md (newData);
			return newData;
		}
	}

	public class MdTest {
		[Test] public void ShoudCopyModel () {
			var model = new Model { A = "A", B = "B" };
			var newModel = model.With (x => x.A = "CA").With (x => x.B = "CB");
			Assert.AreNotSame (model, newModel);
			Assert.AreEqual ("A", model.A);
			Assert.AreEqual ("CA", newModel.A);
		}

		[Test] public void ShouldCopyFModel () {
			var model = new FModel("A", "B");
			var newModel = model.With (x => x.A = "FA").With (x => x.B = "BA");
			Assert.AreNotSame (model, newModel);
			Assert.AreEqual ("A", model.A);
			Assert.AreEqual ("FA", newModel.A);
		}
	}
}


