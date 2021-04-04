## ANNOUNCE

[KSP Recall 0.1.0.0](https://github.com/net-lisias-ksp/KSP-Recall/releases/tag/RELEASE%2F0.1.0.0) is on the Wild, featuring:

* More versatile (and user hackable) mechanism to activate/deactivate the Fixes (i.e: a way to override the safeties checks)
* Allowing the inactivation of the fixes to be persisted on the craft file and savegame, so you can deactivate a fix on some parts and keep them on others on a craft by craft basis
	+ Will allow the user to safely keep playing until a new version with a fix/workaround implemented is not released when things goes south.
* Reenabling support for parts with `ModuleCargoPart`
	+ Not all Stackable parts will not be refunded yet.
* More reliable and robust Game Event handling.
* Compatibility with resource changing Add'Ons (as fuel switches) enhanced.

From 0.1.0.0 and newer, **every fix** can be deactivated on a given part, without affecting the other ones. This change is persisted on the craft file and on the savegame, so you can even deactivate a fix on a part on a craft and not do it on another craft. For people willing to write patches, the name of the attribute is `active`.

The user can also force his/her hand on how and where the fixes is installed by editing `GameData/999_KSP-Recall/KSP-Recall.cfg`. Changing this file will overrule the default installation decisions KSP-Recall does on startup, allowing you use fixes that usually would not be available for your rig. **Use this with caution and prudence**, this can royally screw up your savegames.

A new module, `Refunding`, as well a new "Resource" was introduced for KSP 1.11, to counter attack the miscalculation mentioned above.

When recovering the craft, the badly calculated refunds will still be there, but an additional Resource called "Refunding" will be present ~~stealing~~ giving back the losses.

![](https://user-images.githubusercontent.com/64334/109741166-49a36080-7bab-11eb-8b15-1fe0741f53d4.png)

Users of **EVERY** add'on that implements`IPartCostModifier` and are running on KSP 1.11.x **NEED** to install KSP-Recall - or your Career will be seriously hindered.

Affected (known) add'ons are:

+ [Darth Pointer's Pay to Play](https://github.com/DarthPointer/PayToPlay/)
+ [FreeThinker's Interstellar Fuel Switch](https://github.com/sswelm/KSP-Interstellar-Extended/)
+ [allista's Cargo Accelerators](https://github.com/allista/CargoAccelerators)
+ [All Angel 125 Add'Ons that uses WildBlueTools](https://github.com/Angel-125/WildBlueTools/)
+ [Nathan Kell's Modular Fuel System](https://github.com/NathanKell/ModularFuelSystem/) (and Real Fuels)
+ [IgorZ's Kerbal Inventory System](https://github.com/ihsoft/KIS)
+ [KOS](https://github.com/KSP-KOS/KOS)
+ [Kerbalism](https://github.com/Kerbalism/Kerbalism)
+ [And many, many others](https://github.com/search?o=desc&p=3&q=IPartCostModifier&s=indexed&type=Code) - perhaps Squad's own modules (who knows?)


- - - 

This Release will be published using the following Schedule:

* GitHub, reaching manual installers and users of KSP-AVC first. Right now.
* CurseForge, by this Sunday between noon and dusk (GMT-3)
* SpaceDock (and CKAN users), by this Sunday's night (GMT-3)

The reasoning is to gradually distribute the Release to easily monitor the deployment and cope with eventual mishaps.
