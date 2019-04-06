![image](https://github.com/FredEkstrand/ImageFiles/raw/master/ProjectImgaeHeader.png)
# The Enhanced GroupBox Project

![Version 1.0.0](https://img.shields.io/badge/Version-1.0.0-brightgreen.svg) ![License MIT](https://img.shields.io/badge/Licence-MIT-blue.svg)

## Overview
The Enhanced GroupBox control is a collection of features and styling to provide a richer UI control for application development.

### Features
*	Borders can now have their color and width set.
*	Borders corners can be rounded. 
*	Border rendering can have Dash styles.
*	Border rendering can have LineCap styles.
*	Text can be placed top left, top right, top center, bottom left, bottom right, and bottom center.
*	Text color option.
*	Text back color option.
*	Text back gradient start and end color option with direction.
*	Text border options similar to border options above.
*	Inner border area can be set to a color.
*	Inner border area can have a gradient start and end color option with direction.
*	Defined rendering styles: Standard, Enhanced, Executive, and Header.


## Packaged
The source code is written in C# and targeted for the .Net Framework 4.0 and later. 
### Option 1:
A VSIX installer is created for installation into the toolbox for Visual Studio 2012, 2013, 2015, 2017, 2019

{ No installer yet. }
Installer for the control can be found [here].

### Option 2:
Download the entire project and compile and add to the toolbox yourself.

## Usage
Use of the control is the same as with the basic Visual Studio GroupBox control with the exception of more options to change its appearance. To keep the additional options organized the property grid appearance section have subsection: HeaderElements, BorderElements, and InsideBorderElements. Below is an image showing the parts of the Enhance GroupBox control in relation to the property grid subsections.


![image](https://github.com/FredEkstrand/ImageFiles/raw/master/EnhanceGroupBoxParts.png)

In the image below is a screen shot of the property grid with the parts highlighted. 

![image](https://github.com/FredEkstrand/ImageFiles/raw/master/PropertyGridView.png)

### Examples
Below are some example configurations.
![image](https://github.com/FredEkstrand/ImageFiles/raw/master/EnhanceGroupBoxSamples.png)

## API
Code API can be found [here](http://fredekstrand.github.io/EnhanceGroupBox).

## History
 1.0.0 Initial release into the wild.

## Contributing

If you'd like to contribute, please fork the repository and use a feature
branch. Pull requests are always welcome.

## Contact
Fred Ekstrand
email: fredekstrandgithub@gmail.com

## Licensing
This project is licensed under the MIT License - see the LICENSE.md file for details.

## Inspiration
The project came about through the need in an application development to allow the user to easily and cleanly locate controls in defined sections. The Visual Studio standard GroupBox control is limited in setting its appearance to allow for an intuitive UI flow for the end user. These limitation lead to quick extension to the GroupBox which lead to even more further extensions of the control from project to project. This caused me to move from quick extensions to a more formal enhance GroupBox control. In the development of the enhance GroupBox control I had taken ideas from some other works and incorporated them to provide range of features divided into styles.<br/>

##### Sources for project feature ideas:
**The Grouper** - A Custom Groupbox Control By: M@dHatter, 23 Jan 2006
**Header-only GroupBox** By: Matthew Adams, 22 Sep 2001
**RoundedGroupBox Winforms Rounded Group Box Control** By: Kevin Carbis
**Extended Graphics** - An implementation of Rounded Rectangle in C# By: Arun Reginald Zaheeruddin

