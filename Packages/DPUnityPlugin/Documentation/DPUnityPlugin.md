# DPUnityPlugin Documentation #

## General plugin setup

1. Add the __dpCorrection script__ as component either directly to your main camera (prefered) or to any other game object and assign the maincamera to the dpCorrection script camera slot.
The plugin will disable this camera and replace it by a complete camera rig with one camera for each projector.
Most camera settings are copied to the individual cameras in the camera rig.
2. Set the path to the correction setup config.xml (relative or absolute pathes supported). It is advisable to put that config.xml in a subfolder of the unity project along with all correction data. That way you can just copy that correction data folder into the build folder later on, so that the build application still find the correction data, no matter where they are copied.
3. Set an optional origin. When set, the cameras, and projection shapes will be placed relative to the origin instead of the main camera, this is especially convenient for dynamic warping setups.

## Builds preparation

1. After first build of the unity project, copy your correction data path into the build folder, so the build has access to correction data at the correct relative path.

## Settings ##

The settings are available from the dpcorrection script, when assigned to a game object, prefarable the main camera.

| Parameter | Description |
| --------- | ----------- |
| Configuration File | The path to the config.xml file generated with domeprojection.com ProjectionTools. Can be absolute or relative to project/build path. |
| Camera | The camera which will be replaced by the camera rig configured in the correction data set. When the script is attatched to a camera, this is optional. For __ProjectionMappimg dynamic warping__ this camera is used to render the content to an offscreen buffer to be put as texture on the screen. |
| Origin | The origin everything is referenced to in dynamic warping modes. Only used for __3D dynamic warping__  and __ProjectionMappimg dynamic warping__ |
| Layer | The layer to be used for rendering the projection mapping path. Ensure that this layer is available (maybe an additional layer must be created) and NOT in use by any standard camera (remove it from cameras culling mask). Only used for  __ProjectionMappimg dynamic warping__. |
| Render Correction Data | Enable additional visualizations of correction data and camera rig in Unity Scene view, when in play mode. |

## Correction Data ##

The correction data used within this package can be generated by a dedicated Unity exporter in [domeprojection.com ProjectionTools](https://www.domeprojection.com/products/projection-tools) Mapper2d, Mapper3d or MapperPM since version 4.2.

## Configuration File ##

In some cases additional adjustments might be needed in the configuration file.

Example of a static warping config file:

    <?xml version="1.0" encoding="utf-8"?>
    <dpCorrection>
        <type>0</type>
        <gamma>2.2</gamma>
        <offset>0,0,0</offset>
        <loglevel>1</loglevel>
        <channel id="0" blending="blending_0.png" frustum="frustum_0.csv" warpmap="warpmap_0.csv" display="0" viewport="0,0,1,1"></channel>
        <channel id="1" blending="blending_1.png" frustum="frustum_1.csv" warpmap="warpmap_1.csv" display="1" viewport="0,0,1,1"></channel>
        <channel id="2" blending="blending_2.png" frustum="frustum_2.csv" warpmap="warpmap_2.csv" display="2" viewport="0,0,1,1"></channel>
    </dpCorrection>

### type ###

Defines the general structure of correction data for different use-cases.

| Value | Description 
| ----- | ----------- 
| 0     | 3D/PM static warping 
| 1     | 3D dynamic warping 
| 2     | 2D orthographic static warping 
| 3     | ProjectionMappimg dynamic warping 

### gamma ###

Define the Projector/Display gamme. This is important for correct blending and black-level correction. Correction data and content rendered in Unity is assumed to be in the same gamma-space.

### offset ###

Allows to offset the camera rig position in the case of static warping.
In ProjectionTools coordinate system (right, front, up in millimeters)

### loglevel ###

| Value | Description 
| ----- | ----------- 
| 0     | logging is switched off [default]
| 1     | Unity display number and domeprojection.com ProjectionTools channel number will be rendered on top of each channel

### channel ###

The config file can define multiple channels. One for each output.

Parameter | Description
 --- | ---
id | unique id for each channel, usually the same id as projector number in ProjectionTools
display | The Unity display, this channel should be shown on
viewport | Positioning of the channel on the display. Can be used to restrict a channel to a subregion of a display.  Multiple channels can be shown on one display,this way. Format: (bottom,left,width,height) as normalized coordinates relative to bottom-left display corner.
blending | Blendfile to be used. Relative to config file. (.png)
blacklevel | Blacklevel correction to be used. (.png, optional)
frustum | Frustum file to be used, relative to config file. (.csv)
warpmap | Warpmap file to be used, relative to config file. (.csv)
shape | 3D shape of channel (.obj, dynamic warping only)
target | Target rectangle, for generating frustum dynamically for a given eyepoint. (.csv, dynamic warping only)

### mapping_0 ###

Screen surface for __ProjectionMappimg dynamic warping__. (.obj in ProjectionTools coordinate system)

    <?xml version="1.0" encoding="utf-8"?>
    <dpCorrection>
        ...
        <mapping_0>mapping_0.obj</mapping_0>
        ...
    </dpCorrection>
 
## Channel/Display Assignment ##

The display order in Unity might differ from the channels in domeprojection.com ProjectionTools.
The config allows to compensate this discrepancy.

Each channel in the config.xml file of the correction data comes with an adjustable display attribute which defines the Unity display the correction data will be applied to.

Setting the log level in config.xml to 1 may help finding the correct channel/display assignment. 

    //dpCorrection/loglevel=0: logging is switched off [default]  
    //dpCorrection/loglevel=1: Unity display number and domeprojection.com ProjectionTools channel number will rendered on top

## Commandline parameters ##

It is possible to override the configuration file that, should be loaded by adding -dpConfig option followed by a relative or absolute path to a valid plugin configuration file.

    YourGame.exe -dpConfig exportData/config.xml