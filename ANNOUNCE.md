## ANNOUNCE

[KSP Recall 0.2.0.6](https://github.com/net-lisias-ksp/KSP-Recall/releases/tag/RELEASE%2F0.2.0.6) is on the Wild, featuring:

Fixes for the already published fixes:

- [#26](https://github.com/net-lisias-ksp/KSP-Recall/issues/26) Kerbal going on EVA on Kerbin without helmet instantly dies
- [#25](https://github.com/net-lisias-ksp/KSP-Recall/issues/25) ChillingOut apparently is screwing up KSPIE

Both issues were related to the same feature, `ChillingOut`, besides having different causes. In both cases, less than ideal implementations ended up causing undesired collateral effects.

Thanks to @ss8913 and @rawhide_k for [their](https://forum.kerbalspaceprogram.com/index.php?/topic/192048-18/&do=findComment&comment=4010273) [reports](https://forum.kerbalspaceprogram.com/index.php?/topic/192048-18/&do=findComment&comment=4010617).

### About the Future

Given the current state of affairs on Forum, I think the best line of action for KSP-Recall is to stop pursuing work-arounds for KSP 1.12.x and let others do the job.

So I decided that no KSP 1.12.x specific bugs will be handled anymore by KSP-Recall as it's becoming increasingly harder to diagnose and code fixes relying only on [Clean Room Design](https://en.wikipedia.org/wiki/Clean_room_design) approaches, and recent events suggest that trying different methods can be legally risky (even by, at least in theory, being perfectly legal on USA, Europe and most other countries). 

The current Workarounds will be actively maintained and eventual workarounds for KSP versions up to 1.11.2 (including very old ones, as 1.2.2 and 1.3.1 - I'm still playing on 1.7.3!!) can be considered - mainly because these codebases are very well known already and there's no one else considering supporting them.

There're alternatives for KSP-Recall available on Forum for KSP 1.12.x users, so this is not exactly a settle back - it can be even an improvement, as some of the alternatives are willing to go trought paths I'm not.

As usual, anything going wrong or weird can still be reported here: if it's something on KSP-Recall, **it will be fixed**. If it affects some older KSP version still in use, we can study a workaround or perhaps a fix (these versions will not change for sure anymore!).

For problems on KSP 1.12.x, at least some preliminary diagnosing can be carry out, making easier for some third-party to code a fix. But no implementation will be carried out.

- - - 

This Release will be published using the following Schedule:

* GitHub, reaching manual installers and users of KSP-AVC first. Right now.
* CurseForge, by this Wednesday night (GMT-3) - I hope
* SpaceDock (and CKAN users), by this Thursday's night (GMT-3) - with luck

The reasoning is to gradually distribute the Release to easily monitor the deployment and cope with eventual mishaps.
