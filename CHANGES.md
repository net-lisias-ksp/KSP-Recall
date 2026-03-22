# KSP-Recall :: Changes

* 2026-0321: 0.5.0.3 (LisiasT) for KSP >= 1.4.1
	+ Updates `KSPe.Light` to 2.5.5.2, fixing a lame mistake that prevented it from working on KSP < 1.8.0 🤦
	+ Removes `ModuleManagerWatchDog` from the distribution instead of updating it to the latest.
		- Manual Installers and CurseForge users will install `ModularManagement` where the latest version will always be available.
		- CKAN doesn't install it.
* 2024-0521: 0.5.0.2 (LisiasT) for KSP >= 1.4.1
	+ Fixes an unexpected misbehaviour on KSP >= 1.9.x that, frankly, I don't know how I had missed for so much time.
	+ Closes issues:
		- [#80](https://github.com/net-lisias-ksp/KSP-Recall/issues/80) Merge Craft is getting its surface attachments screwed on 1.9.x and newer 
* 2024-0519: 0.5.0.1 (LisiasT) for KSP >= 1.4.1
	+ ***WITHDRAWN***
		- Because sleepy developers should do more sleeping and less releases! :P
* 2024-0506: 0.5.0.0 (LisiasT) for KSP >= 1.4.1
	+ Rollbacks the PAW changes due an absolutely unexpected, unexplainable and crappy KSP behaviour that only manifested itself on KSP 1.12.x
		- I don't have the slightest clue about what happened yet. 
	+ Closes issues:
		- [#77](https://github.com/net-lisias-ksp/KSP-Recall/issues/77) 
Refunding is not working on 1.12.x!!!
