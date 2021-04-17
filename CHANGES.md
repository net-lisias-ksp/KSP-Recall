# KSP-Recall :: Changes

* 2021-0417: 0.1.0.8 (LisiasT) for KSP >= 1.4.1
	+ **Finally** diagnosed and fixed an issue on refunding Kerbals on Command Seats (or standalones)
	+ Reworks the issue [#16](https://github.com/net-lisias-ksp/KSP-Recall/issues/16).
* 2021-0416: 0.1.0.7 (LisiasT) for KSP >= 1.4.1
	* **Ditched**
* 2021-0413: 0.1.0.6 (LisiasT) for KSP >= 1.4.1
	+ Reworks the Work Around for issue [#16](https://github.com/net-lisias-ksp/KSP-Recall/issues/16), fixing the Stock's over-refunding on `ModuleInventoryPart`.
* 2021-0412: 0.1.0.5 (LisiasT) for KSP >= 1.4.1
	+ Fixes the workaround implemented on 0.1.0.4
* 2021-0411: 0.1.0.4 (LisiasT) for KSP >= 1.4.1
	+ Implements a Work Around for issue [#16](https://github.com/net-lisias-ksp/KSP-Recall/issues/16).
		- O pulled this out from my hat, it's not known yet if this will work on every use case.
		- Further testings as work in progress. Use this with caution for now.
* 2021-0409: 0.1.0.3 (LisiasT) for KSP >= 1.4.1
	+ The problem fixed on 1.0.2 was masking another problem on `Refunding` that, once fixed, regressed the over-billing problem.
		- GameEvents related to vessels don't work as I expected.
		- The solution was to step back a bit, and risking some over-refunding on FMRS on automatic recovery.
		- Sorry about that.
* 2021-0408: 0.1.0.2 (LisiasT) for KSP >= 1.4.1
	+ Pretty stupid mistake on `Refunding` fixed.
	+ Updating KSPe Light.
* 2021-0406: 0.1.0.1 (LisiasT) for KSP >= 1.4.1
	+ Minor revision to make life easier for Package Managers as CKAN.
		- Will allow installing on any KSP >= 1.4.1, even by not having (yet :P) any fix for them.
	+ Closes Isssues:
		- [#14](https://github.com/net-lisias-ksp/KSP-Recall/issues/14) Make Recall safe to be installed on any KSP version instead of yelling about not being compatible  
* 2021-0404: 0.1.0.0 (LisiasT) for KSP >= 1.8.0
	+ More versatile (and user hackable) mechanism to activate/deactivate the Fixes (i.e: a way to override the safeties checks)
	+ Allowing the inactivation of the fixes to be persisted on the craft file and savegame, so you can deactivate a fix on some parts and keep them on others on a craft by craft basis
		- Will allow the user to safely keep playing until a new version with a fix/workaround implemented is not released when things goes south.
	+ Reenabling support for parts with `ModuleCargoPart`
		- Not all Stackable parts will not be refunded yet.
	+ More reliable and robust Game Event handling.
	+ Compatibility with resource changing Add'Ons (as fuel switches) enhanced.
