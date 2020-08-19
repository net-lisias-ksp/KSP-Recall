# KSP-Recall :: Known Issues

*  Wheels and Landing Legs **also** drifts, but due different reasons - so, in fact, we have **two** sources of drifting on KSP >= 1.8 right now. But the problem `Driftless` solves helps, and helps a lot, on preventing the situation where the Legs and Wheels start to be a problem!
	+ So, yeah... I was wrong, this thing is more a fix than a workaround by this time! :)  
* I was wrong too on adjusting the *Maximum Physics Delta-Time Per Frame* to 0.03. It make things worse on CPUs under pressure!
	+ So, if you have CPU to spare (i.e., never gets the yellow time on the top left of the screen), raising MPDTPF can help.
	+ If your CPU is neat the limit, it will make things worse!
* The part count on the current scene **is the real key**
	+ Big crafts tend to drift on loaded CPUs because somehow the MPDTPF appears to be abbreviating the FixedUpdate processing, and no not all parts are zeroed.
		- If I'm right, high count crafts ended up with only some of the parts fixed, and then the driftness induced by the others prevails
		- And the part count of the whole **scene** (i.e., everything that is unpacked and it's on the current Physics Range) is what counts, no the part count of the current craft!
	+ Kerbals are just another part.
		- So they will drift or not depending in where on the list of parts for the current Scene they are! 
* KSP-Recall *WORKS FINE WITH* Module Manager.
	+ [Anyone telling you](http://ksp.lisias.net/add-ons/ModuleManager/WatchDog/Screen%20Shot%202020-07-14%20at%2002.14.51.png) you need an alternative fork of MM is telling you nonsense.

- - -

* RiP : Research in Progress
* WiP : Work in Progress
