using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insight
{

	public static class ParticleSystemExtension
	{
		public static Material getMaterial(this ParticleSystem particleSystem) {

			return particleSystem.GetComponent<Renderer>().material;

		}
	}

}
