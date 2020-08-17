## ANNOUNCE

A new Work Around is now available on [KSP Recall 0.0.4.0](https://github.com/net-lisias-ksp/KSP-Recall/releases) and it's available for the brave Kerbonauts willing to risk their SAS with this stunt.

On KSP 1.8 and newer, parked crafts (even without wheels) drifts the Heading to the left (or to the right, it's kinda random) when leaved at their own. The drifting is a bit severe, reaching many degrees by minute.

KSP Recall 0.0.4 introduces a fix that alleviate or cancel this drifting. On my tests, I managed to leave a craft alone for 15 minutes without drifting a single degree.

Note: Wheels and Landing Legs **also** drifts, but different reasons - so, in fact, we have **two** sources of drifting on KSP >= 1.8 right now. The present version does not (yet) fixes the wheels' drift (besides helping a very little bit, as there's one less source of drifting now).

Additionally, a smarter way of selectively patching the parts in need of the Work Arounds is now in effect, making it feasible to install KSP Recall on any KSP version (needing it or not) without impacting the loading time: the parts are now **selectively** applied, instead of being applied not matter what and then being cleaned up by code. The cleaning code is still there, anyway, as a last line of defense.


Good Luck!  

- - - 

This Release will be published using the following Schedule:

* GitHub, reaching first manual installers and users of KSP-AVC. Right now.
* CurseForge, by Saturdat night
* SpaceDock (and CKAN users), by Sunday afternoon.

The reasoning is to gradually distribute the Release to easily monitor the deployment and cope with eventual mishaps.
