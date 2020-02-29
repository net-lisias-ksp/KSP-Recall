# KSP-Recall

TBD

## Installation Instructions

To install, place the GameData folder inside your Kerbal Space Program folder:

* **REMOVE ANY OLD VERSIONS OF THE PRODUCT BEFORE INSTALLING**, including any other fork:
	+ Delete `<KSP_ROOT>/GameData/000_KSP-Recall`
* Extract the package's `GameData/` folder into your KSP's as follows:
	+ `<PACKAGE>/GameData/000_KSP-Recall/*` --> `<KSP_ROOT>/GameData/000_KSP-Recall`
		- Overwrite any preexisting file.

The following file layout must be present after installation:

```
<KSP_ROOT>
	[GameData]
		[000_KSP-Recall]
			[Plugins]
				...
			[patches]
				...
			CHANGE_LOG.md
			LICENSE
			NOTICE
			README.md
			KSP-Recall.version
		ModuleManager.dll
		...
	KSP.log
	PastDatabase.cfg
	...
```


### Dependencies

* Module Manager 3.0.7 or later
	+ **NOT** Included
