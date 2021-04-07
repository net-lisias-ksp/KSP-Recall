# KSP-Recall :: Changes

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
