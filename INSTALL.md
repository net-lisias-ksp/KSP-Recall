# KSP-Recall

Recall for KSP blunders, screw ups and borks.

Aims to fix Stock misbehaviours the most seamlessly as possible.

## Installation Instructions

To install, place the GameData folder inside your Kerbal Space Program folder:

* **REMOVE ANY OLD VERSIONS OF THE PRODUCT BEFORE INSTALLING**, including any other fork:
	+ Delete `<KSP_ROOT>/GameData/999_KSP-Recall`
* Extract the package's `GameData/` folder into your KSP's as follows:
	+ `<PACKAGE>/GameData/999_KSP-Recall/*` --> `<KSP_ROOT>/GameData/999_KSP-Recall`
		- Overwrite any preexisting file.
* Extract the included dependencies (optional)
	+ `<PACKAGE>/GameData/ModuleManagerWatchDog` --> `<KSP_ROOT>/GameData`
	+ `<PACKAGE>/GameData/666_ModuleManagerWatchDog.dll` --> `<KSP_ROOT>/GameData`
		- Overwrite any preexisting file.

The following file layout must be present after installation:

```
<KSP_ROOT>
	[GameData]
		[999_KSP-Recall]
			[Plugins]
				...
			[patches]
				...
			CHANGE_LOG.md
			LICENSE
			NOTICE
			README.md
			KSP-Recall.version
		[ModuleManagerWatchDog]
			...
		ModuleManager.dll
		ModuleManagerWatchDog.dll
		...
	KSP.log
	PastDatabase.cfg
	...
```


### Dependencies

* [Module Manager Watch Dog](https://github.com/net-lisias-ksp/ModuleManagerWatchDog/releases)
	+ Included
	+ Licensed to KSP-Recall under [SKL 1.0](https://ksp.lisias.net/SKL-1_0.txt)
* Module Manager 4.0.2 or later
	+ **NOT** Included
