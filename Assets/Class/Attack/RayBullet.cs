using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayBullet : Damager {


	Transform transformInicial;
	public float bulletSpeed = 10.0f;
	public float lenght = 2.0f;
	Vector3 origen;
	Vector3 direccion;

	LineRenderer lineRenderer;

	void Awake()
	{
		transformInicial = transform;
		origen = transformInicial.position;
		direccion = transformInicial.up * lenght;

		lineRenderer = GetComponent<LineRenderer>();
	}


	void LateUpdate()
	{

		origen = Vector3.Lerp(origen, origen + transformInicial.up, bulletSpeed * Time.deltaTime);

		Ray rayBullet = new Ray(origen, direccion);
		RaycastHit rayBulletHit;

		Physics.Raycast(rayBullet, out rayBulletHit, lenght);

		Debug.DrawRay(origen, direccion, Color.blue);

		lineRenderer.SetPosition(0, origen);
		lineRenderer.SetPosition(1, origen + direccion);

		if(rayBulletHit.collider)
		{
			Destroyable col = rayBulletHit.collider.gameObject.GetComponent<Destroyable>();
			if(col)
			{
				if(col.IsDestroyable)
				{
					col.ReceiveDamage(Damage);

				}
			}

			Destroy(gameObject);
		}


	}
}
