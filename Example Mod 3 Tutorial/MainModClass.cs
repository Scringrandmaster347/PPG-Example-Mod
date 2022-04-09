using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Mod
{
    public class MainModClass
    {
        //Some Sprites which will be loaded here to be made accesible in other classes(like the one of our custom Item)...
        public static Sprite AdvancedItem1 = ModAPI.LoadSprite("Textures/AdvancedItem1.png");
        public static Sprite AdvancedItem2 = ModAPI.LoadSprite("Textures/AdvancedItem2.png");
        public static Sprite AdvancedItem3 = ModAPI.LoadSprite("Textures/AdvancedItem3.png");
        public static Sprite AdvancedItem4 = ModAPI.LoadSprite("Textures/AdvancedItem4.png");
        public static Sprite AdvancedItem5 = ModAPI.LoadSprite("Textures/AdvancedItem5.png");

        public static string ExampleModCategory1 = "<color=red>Example Mod<color=white>";
        //public static string ExampleModCategory2 = "<color=green>Another Example Mod<color=white>";

        public static void Main()
        {
            Liquid.Register("OrangeJuiceBlood", (Liquid)new OrangeJuiceBlood());//Basically registers our Liquid...
            Liquid.Register("LimeSlime", (Liquid)new LimeSlime());

            CategoryBuilder.CreateCategory(ExampleModCategory1, "This is a cool Description!!!", ModAPI.LoadSprite("ModIcon.png"));//Actually registers our Category
            //CategoryBuilder.CreateCategory(ExampleModCategory2, "!!!", ModAPI.LoadSprite("ModIcon.png"))

            //Our First Custom Human...
            ModAPI.Register(new Modification()
            {
                OriginalItem = ModAPI.FindSpawnable("Human"), //We basically derive from an already existing item in the game to create a new on...in this case we have a human...
                NameOverride = "Custom Human 1", // the name of the custom Human...
                NameToOrderByOverride = "Custom Human 1", //Sortingorder in the Category....0 comes before 1 one as an example and Custom Human 1 would come befor the Human in the Menu because c is before h in´the alphabet...
                DescriptionOverride = "This is a custom description to a custom Human...", //Explains itself, a simple description of your Human / Item / ect......
                CategoryOverride = ModAPI.FindCategory(ExampleModCategory1), // This is the Category in which our Human should appear in...
                ThumbnailOverride = ModAPI.LoadSprite("Textures/CustomHuman1Thumbnail.png"), //The Thumbnail...
                AfterSpawn = (Instance) =>
                {
                    var skin = ModAPI.LoadTexture("Textures/CustomHuman1Skin.png"); //The Skin texture for the Human...
                    var flesh = ModAPI.LoadTexture("Textures/CustomHuman1Flesh.png"); //The Flesh texture for the Human...
                    var bone = ModAPI.LoadTexture("Textures/CustomHuman1Bone.png"); //The Bone texture for the Human...

                    var PersonBehaviourComponent = Instance.GetComponent<PersonBehaviour>(); // this gets the personbehaviour component of the Human we want to modify...

                    PersonBehaviourComponent.SetBodyTextures(skin, flesh, bone, 1); //This sets the textures of our custom Human to the of our own, instead of just the normal ones...The last value or the 1 has something to do with the texture size or so...

                    PersonBehaviourComponent.SetBruiseColor(178, 0, 178); //Here we set the R.G.B (Red, Green, Blue) values for our bruises -> this would be purple like color...
                    PersonBehaviourComponent.SetSecondBruiseColor(154, 0, 7);
                    PersonBehaviourComponent.SetThirdBruiseColor(207, 206, 120);
                    PersonBehaviourComponent.SetRottenColour(202, 199, 104);
                    PersonBehaviourComponent.SetBloodColour(108, 0, 4); // this doesn't set the Liquid color of the human blood...if we want to change the blood type or color however, we musst then create our own "liquid"...,which I will show in a later Tutorial...

                    DecalDescriptor DecalDescriptor1 = ScriptableObject.CreateInstance<DecalDescriptor>(); //Overrides or gets the DecalDescriptor, which handles some stuff about the Blood, ect...

                    foreach (LimbBehaviour Limb in PersonBehaviourComponent.Limbs) //Gets every Personbehaviour in each Limb...
                    {
                        DecalDescriptor1.Sprites = Limb.BloodDecal.Sprites; //Sets BloodDecalsprites...
                        DecalDescriptor1.IgnoreRadius = Limb.BloodDecal.IgnoreRadius; //something about the radius of the Decals...
                        DecalDescriptor1.Color = new Color(OrangeJuiceBlood.RedColorValue, OrangeJuiceBlood.GreenColorValue, OrangeJuiceBlood.BlueColorValue, OrangeJuiceBlood.TransparencyValue); //Sets the Decal Color to the of our own custom Liquid...(R,G,B,A)
                        Limb.BloodDecal = DecalDescriptor1; //Overrides foreach Limb the DecalDescriptor....
                        Limb.BloodLiquidType = "OrangeJuiceBlood"; //Sets the Bloodtype to our own custom Liquid
                        Limb.CirculationBehaviour.Drain(Limb.CirculationBehaviour.Limits.y); //Drains every other Liquid in the Human...
                        double num = (double)Limb.CirculationBehaviour.AddLiquid(Liquid.GetLiquid("OrangeJuiceBlood"), Limb.CirculationBehaviour.Limits.y); //Fills the Human with our Custom Blood instead...
                    }
                }
            });

            //Our Second Custom Human
            ModAPI.Register(new Modification()
            {
                OriginalItem = ModAPI.FindSpawnable("Human"), //We basically derive from an already existing item in the game to create a new on...in this case we have a human...
                NameOverride = "Custom Human 2", // the name of the custom Human...
                NameToOrderByOverride = "Custom Human 2", //Sortingorder in the Category....0 comes before 1 one as an example and Custom Human 1 would come befor the Human in the Menu because c is before h in´the alphabet...
                DescriptionOverride = "This is a custom description to another custom Human...", //Explains itself, a simple description of your Human / Item / ect......
                CategoryOverride = ModAPI.FindCategory(ExampleModCategory1), // This is the Category in which our Human should appear in...
                ThumbnailOverride = ModAPI.LoadSprite("Textures/CustomHuman2Thumbnail.png"), //The Thumbnail...
                AfterSpawn = (Instance) =>
                {
                    var skin = ModAPI.LoadTexture("Textures/CustomHuman2Skin.png"); //The Skin texture for the Human...
                    var flesh = ModAPI.LoadTexture("Textures/CustomHuman2Flesh.png"); //The Flesh texture for the Human...
                    var bone = ModAPI.LoadTexture("Textures/CustomHuman2Bone.png"); //The Bone texture for the Human...

                    var PersonBehaviourComponent = Instance.GetComponent<PersonBehaviour>(); // this gets the personbehaviour component of the Human we want to modify...

                    PersonBehaviourComponent.SetBodyTextures(skin, flesh, bone, 1); //This sets the textures of our custom Human to the of our own, instead of just the normal ones...The last value or the 1 has something to do with the texture size or so...

                    PersonBehaviourComponent.SetBruiseColor(1, 0, 178); //Here we set the R.G.B (Red, Green, Blue) values for our bruises -> this would be a blue like color...
                    PersonBehaviourComponent.SetSecondBruiseColor(124, 0, 70);
                    PersonBehaviourComponent.SetThirdBruiseColor(27, 26, 120);
                    PersonBehaviourComponent.SetRottenColour(202, 199, 104);
                    PersonBehaviourComponent.SetBloodColour(71, 200, 4); // this doesn't set the Liquid color of the human blood...if we want to change the blood type or color however, we musst then create our own "liquid"...,which I will show in a later Tutorial...

                    DecalDescriptor DecalDescriptor1 = ScriptableObject.CreateInstance<DecalDescriptor>(); //Overrides or gets the DecalDescriptor, which handles some stuff about the Blood, ect...

                    foreach (LimbBehaviour Limb in PersonBehaviourComponent.Limbs) //Gets every Personbehaviour in each Limb...
                    {
                        DecalDescriptor1.Sprites = Limb.BloodDecal.Sprites; //Sets BloodDecalsprites...
                        DecalDescriptor1.IgnoreRadius = Limb.BloodDecal.IgnoreRadius; //something about the radius of the Decals...
                        DecalDescriptor1.Color = new Color(LimeSlime.RedColorValue, LimeSlime.GreenColorValue, LimeSlime.BlueColorValue, LimeSlime.TransparencyValue); //Sets the Decal Color to the of our own custom Liquid...(R,G,B,A)
                        Limb.BloodDecal = DecalDescriptor1; //Overrides foreach Limb the DecalDescriptor....
                        Limb.BloodLiquidType = "LimeSlime"; //Sets the Bloodtype to our own custom Liquid
                        Limb.CirculationBehaviour.Drain(Limb.CirculationBehaviour.Limits.y); //Drains every other Liquid in the Human...
                        double num = (double)Limb.CirculationBehaviour.AddLiquid(Liquid.GetLiquid("LimeSlime"), Limb.CirculationBehaviour.Limits.y); //Fills the Human with our Custom Blood instead...
                    }
                }
            });

            ModAPI.Register(new Modification()//Our First Simple Custom Item...
            {
                OriginalItem = ModAPI.FindSpawnable("Brick"),
                NameOverride = "Red Brick",
                NameToOrderByOverride = "RB0",
                DescriptionOverride = "It's a Red Brick11!1!11!1!1!!!1111!!!!!!",
                CategoryOverride = ModAPI.FindCategory(ExampleModCategory1),
                ThumbnailOverride = ModAPI.LoadSprite("Textures/RedBrickThumbnail.png"),
                AfterSpawn = (Instance) =>
                {
                    Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/RedBrick.png");
                    Instance.GetComponent<PhysicalBehaviour>().RefreshOutline();//Refreshes the Outline...
                    Instance.FixColliders();//Alligns the Colldiers with the Texture of the Item properly

                    Instance.GetComponent<PhysicalBehaviour>().Properties = ModAPI.FindPhysicalProperties("Ceramic");//Sets the Physicalproperties to the customtype we want it to be...Mostly noticable ingame with the sounds that change with it btw...
                }
            });

            ModAPI.Register(new Modification()//Our First Advanced Custom Item...
            {
                OriginalItem = ModAPI.FindSpawnable("Rod"),
                NameOverride = "Surrealistic Dice",
                NameToOrderByOverride = "SUD0",
                DescriptionOverride = "It's a weird surreal Dice....I guess...",
                CategoryOverride = ModAPI.FindCategory(ExampleModCategory1),
                ThumbnailOverride = ModAPI.LoadSprite("Textures/AdvancedItemThumb.png"),
                AfterSpawn = (Instance) =>
                {
                    Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/AdvancedItem1.png");
                    Instance.GetComponent<PhysicalBehaviour>().RefreshOutline();//Refreshes the Outline...
                    Instance.AddComponent<CollisionRandomizerItem>();
                    Instance.FixColliders();//Alligns the Colldiers with the Texture of the Item properly

                    Instance.GetComponent<PhysicalBehaviour>().Properties = ModAPI.FindPhysicalProperties("Ceramic");//Sets the Physicalproperties to the customtype we want it to be...Mostly noticable ingame with the sounds that change with it btw...
                }
            });
        }
    }

    public class CategoryBuilder
    {
        public static void CreateCategory(string Name, string Description, Sprite Icon)//This Function will register our custom Category...basically..
        {
            CatalogBehaviour objectOfType = UnityEngine.Object.FindObjectOfType<CatalogBehaviour>();
            if (!((UnityEngine.Object)((IEnumerable<Category>)objectOfType.Catalog.Categories).FirstOrDefault<Category>((Func<Category, bool>)(c => c.name == Name)) == (UnityEngine.Object)null))
                return;
            Category Instance = ScriptableObject.CreateInstance<Category>();
            Instance.name = Name;
            Instance.Description = Description;
            Instance.Icon = Icon;
            Category[] CategoryArray = new Category[objectOfType.Catalog.Categories.Length + 1];
            Category[] Categories = objectOfType.Catalog.Categories;
            for (int X = 0; X < Categories.Length; ++X)
                CategoryArray[X] = Categories[X];
            CategoryArray[CategoryArray.Length - 1] = Instance;
            objectOfType.Catalog.Categories = CategoryArray;
        }
    }

    public class CollisionRandomizerItem : MonoBehaviour
    {
        //Code here the Stuff you want the Custom Item to do...
        /*
        public void Start()
        {
        //Gets called at the Start
        }
        public void Update()
        {
        //Gets called during each frame...btw be carefull with what code you put in here, since unoptimized code can cause real performance issues and so...
        }
        public void Use()
        {
        //Gets called when the Player activates an Object and so basically...
        }
        */
        public void OnCollisionEnter2D(Collision2D Collision)//Detects if it enters a collision...
        {
            if (Collision.transform.GetComponent<CollisionRandomizerItem>() == true)//If the Colliding Object has the CollisionRandomizerItem attached...
            {
                int Random = UnityEngine.Random.Range(0, 5);//0,1,2,3,4 = 5 Possible ways... 5 never gets reached because int is kinda strange in that... but basically from 0 to -1 from the number to reach basically...

                if (Random == 0)
                {
                    transform.GetComponent<SpriteRenderer>().sprite = MainModClass.AdvancedItem1;
                }
                if (Random == 1)
                {
                    transform.GetComponent<SpriteRenderer>().sprite = MainModClass.AdvancedItem2;
                }
                if (Random == 2)
                {
                    transform.GetComponent<SpriteRenderer>().sprite = MainModClass.AdvancedItem3;
                }
                if (Random == 3)
                {
                    transform.GetComponent<SpriteRenderer>().sprite = MainModClass.AdvancedItem4;
                }
                if (Random == 4)
                {
                    transform.GetComponent<SpriteRenderer>().sprite = MainModClass.AdvancedItem5;
                }
                //Basically a random Fuction, which decides the Sprite if the Item collides with another that has the CollisionRandomizerItem attached to it basically
                transform.GetComponent<PhysicalBehaviour>().RefreshOutline();
                transform.gameObject.FixColliders();
                //Fixes the Outlines and Colliders after the Sprite has changed or so...
            }
        }
    }

    public class LimeSlime : Blood
    {
        public new const string ID = "LimeSlime"; //The ID of Custom Blood...
        public new const float RedColorValue = 0.25f; //Red Value...
        public new const float GreenColorValue = 0.8f; //Green Value...
        public new const float BlueColorValue = 0.0f; //Blue Value...
        public new const float TransparencyValue = 1.0f; //The "Transperency" (Alpha) Value...1.0f is visible...0.5f is transparent...and 0.0f is invisible

        public LimeSlime() => this.Color = new Color(RedColorValue, GreenColorValue, BlueColorValue, TransparencyValue); //Sets the Color basically....

        public override void OnEnterContainer(BloodContainer container)
        {
            //do something, if our Liquid enters a Liquidcontainer...
        }

        public override void OnEnterLimb(LimbBehaviour limb)
        {
            //do something, if our Liquid enters any type of Limb from an Entity(humans, androids, gorses, ect)...
        }

        public override void OnExitContainer(BloodContainer container)
        {
            //do something, if our Liquid leaves a Liquidcontainer...
        }
    }

    public class OrangeJuiceBlood : Blood
    {
        public new const string ID = "OrangeJuiceBlood"; //The ID of Custom Blood...
        public new const float RedColorValue = 0.825f; //Red Value...
        public new const float GreenColorValue = 0.7f; //Green Value...
        public new const float BlueColorValue = 0.0f; //Blue Value...
        public new const float TransparencyValue = 1.0f; //The "Transperency" (Alpha) Value...1.0f is visible...0.5f is transparent...and 0.0f is invisible

        public OrangeJuiceBlood() => this.Color = new Color(RedColorValue, GreenColorValue, BlueColorValue, TransparencyValue); //Sets the Color basically....

        public override void OnEnterContainer(BloodContainer container)
        {
            //do something, if our Liquid enters a Liquidcontainer...
        }

        public override void OnEnterLimb(LimbBehaviour limb)
        {
            //do something, if our Liquid enters any type of Limb from an Entity(humans, androids, gorses, ect)...
            if (limb.SpeciesIdentity == "Android")
                limb.PhysicalBehaviour.Charge += 0.1f;
            if (!(limb.SpeciesIdentity == "Gorse"))
                return;
            limb.Health -= 0.25f;
        }

        public override void OnExitContainer(BloodContainer container)
        {
            //do something, if our Liquid leaves a Liquidcontainer...
        }
    }
}