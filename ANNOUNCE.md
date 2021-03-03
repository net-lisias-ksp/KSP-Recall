## ANNOUNCE

[KSP Recall 0.0.7.0 **BETA**](https://github.com/net-lisias-ksp/KSP-Recall/releases/tag/PRE-RELEASE%2F0.0.7.0) in on the Wild, featuring:

* A (dirty) workaround for the KSP 1.11.x faulty calculation of Funds on recovering crafts (Issue [#12](https://github.com/net-lisias-ksp/KSP-Recall/issues/12))

A new module, `Refunding`, as well a new "Resource" was introduced for KSP 1.11, to counter attack the miscalculation mentioned above.

When recovering the craft, the badly calculated refunds will still be there, but an additional Resource called "Refunding" will be present ~~stealing~~ giving back the losses.

![](https://user-images.githubusercontent.com/64334/109741166-49a36080-7bab-11eb-8b15-1fe0741f53d4.png)

### DANGER, WILL ROBINSON, DANGER

This is a preliminary **BETA** version intended to be tested on the field. The ~~kludge~~, I mean, the workaround worked on controlled environments, but I need to test this thing on the field in order to check both the effectiveness and safety of the stunt.

**PLEASE, PRETTY PLEASE** USE THIS ONLY ON DISPOSABLE SAVEGAMES.

Additionally, the solution doesn't works, **YET**, for every add'on on the wild - currently PartModules that changes the cost of the part on the flight scene may be missed. The next Release, 0.0.7.1, will support them but now I need to check how this thing will behave on the field first.

Add'On authors that make use of the `IPartCostModifier` please report your findings on the Issue [#12](https://github.com/net-lisias-ksp/KSP-Recall/issues/12).

I need your feedback.

- - - 

This Release will be published using the following Schedule:

* GitHub, reaching manual installers first and users of KSP-AVC. Right now.
* CurseForge, will not be published.
* SpaceDock (and CKAN users), will not be published.
