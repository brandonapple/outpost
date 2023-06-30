using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandInBullet : MonoBehaviour
{

    private IEnumerator Start()
    {
       // List<Monster> monsters = MonsterManager.monstersInView;
        Base.instance.InvincibleShieldSwitchOn();


        //foreach (Monster monster in monsters)
        //{
        //    TopLaserBullet topLaserBullet = Instantiate(Resources.Load<TopLaserBullet>("Bullets/topLaserBullet"));
        //    topLaserBullet.targetMonster = monster;
        //    topLaserBullet.spike = true;
        //}
        

        for (int i = 0; i < 6; i++)
        {
            FighterPlane fighterPlane = Instantiate(Resources.Load<FighterPlane>("Bullets/fighterPlane"));
            float angle = (float)i / 6 * (Mathf.PI * 2);
            fighterPlane.transform.position = new Vector3(Mathf.Cos(angle), .5f, Mathf.Sin(angle));
            yield return new WaitForSeconds(.1f);
        }


        yield return new WaitForSeconds(4);
        Base.instance.InvincivleShieldSwitchOff();
        Destroy(gameObject);
    }
}
