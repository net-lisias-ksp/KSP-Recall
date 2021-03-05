## ANNOUNCE

[KSP Recall 0.0.7.3](https://github.com/net-lisias-ksp/KSP-Recall/releases/tag/PRE-RELEASE%2F0.0.7.0) is on the Wild, featuring:

* A (dirty) workaround for the KSP 1.11.x faulty calculation of Funds on recovering crafts (Issue [#12](https://github.com/net-lisias-ksp/KSP-Recall/issues/12))
* Fixes a deployment mishap for CurseForge

A new module, `Refunding`, as well a new "Resource" was introduced for KSP 1.11, to counter attack the miscalculation mentioned above.

When recovering the craft, the badly calculated refunds will still be there, but an additional Resource called "Refunding" will be present ~~stealing~~ giving back the losses.

![](https://user-images.githubusercontent.com/64334/109741166-49a36080-7bab-11eb-8b15-1fe0741f53d4.png)

Users of **EVERY** add'on that implements`IPastCostModifier` and are running on KSP 1.11.x **NEED** to install KSP-Recall - or your Career will be seriously hindered.

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
* CurseForge, by Saturday's noon (GMT-3)
* SpaceDock (and CKAN users), by Saturday's night (GMT-3)

The reasoning is to gradually distribute the Release to easily monitor the deployment and cope with eventual mishaps.
