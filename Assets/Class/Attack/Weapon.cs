using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	// Use this for initialization
	public GameObject bulletPrefab;
	public Transform bulletEmmitter;

	public float coolDownTime = 3.0f;
	public float coolDownRate = 1.0f;

	public float timeBetweenBullets = 0.2f;
	public float bulletSpeed = 100.0f;
	public float bulletLifeTime = 3.0f;
	public int maxBullets = 5;
	public int Damage = 1;

	public string bulletLayerName;
	public bool autoAttack;


	// Update is called once per frame


	void Update()
	{

		if(autoAttack)
			AutoShoot();

		CoolDownCheck();
	}



	int bulletsShot;
	float timeToShoot;
	bool coolingDown;
	float coolingDownTime;
	public void AutoShoot()
	{
		if(!coolingDown)
		{
			timeToShoot += Time.deltaTime;

			if(timeToShoot > timeBetweenBullets)
			{
				GameObject bullet = Instantiate(bulletPrefab, bulletEmmitter.position, 
					bulletEmmitter.rotation);
					bulletsShot++;

				Rigidbody bulletRidigBody = bullet.GetComponent<Rigidbody>(); 
				bulletRidigBody.AddForce(bullet.transform.up * bulletSpeed);

				bullet.layer = LayerMask.NameToLayer(bulletLayerName);

				Destroy(bullet, bulletLifeTime);
				timeToShoot = 0.0f;
			}
			if(bulletsShot >= maxBullets)
			{
				coolingDown = true;
				bulletsShot = 0;
			}
		}

	}

	public void Shoot()
	{
		if(!coolingDown)
		{
			RayBullet bullet = Instantiate(bulletPrefab, bulletEmmitter.position, 
						bulletEmmitter.rotation).GetComponent<RayBullet>();
						bulletsShot++;
			
			bullet.bulletSpeed = bulletSpeed;
			bullet.Damage = Damage;

			Destroy(bullet, bulletLifeTime);

			if(bulletsShot >= maxBullets)
			{
				coolingDown = true;
				bulletsShot = 0;
			}
		}

	}


	public void Bomb()
	{


	}


	void CoolDownCheck()
	{
		if(coolingDown)
		{
			coolingDownTime -= Time.deltaTime * coolDownRate;
			if(coolingDownTime <= 0)
			{
				coolingDown = false;
			}
		}
		else
		{
			coolingDownTime = coolDownTime;
		}
	}
}
