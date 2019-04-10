################################################################################################
#######																				     #######
#######	These are the instructions to set up an stereoscopy environment for an 8-view 3d #######
####### autoestereoscopic 1920x1080p TV to show an interlaced image on such display.     #######
#######																				     #######
################################################################################################

Objects necessary:

	*	Standard camera.
	*	Quad with the "FullScreenQuadStereoscopy" material attached to it.
	*	Camera with a renderToTexture.

First of all you need an standard camera placed anywhere on your virtual world. The place does not matter since the cam function is only to render the texture of the Quad in clipspace.
Then, it is important to place the Quad within the frustum of that Camera, otherwise the quad wont be rendered thus there will be no interlaced image.
 If you were to use other camera instead of the prefab on Unity versions higher than 5.2, be sure tto check the near plane of the camera rendering the scene and its far plane are reallyc loser and the quad is contained within. 

After that you should configure your player/moving object/mainCameraAnchor by adding as camera with the renderToTexture and the script "StereoscopicCamRackGenerator". the camera parameter in the script you just added is the
camera you have just created.


You wont be able to see properly in the game tab, since it is not the proper resolution but it is intuitive enough. In order to see properly is a rather cumbersome proccess in which you have to make an executable
 and display it on the TV.