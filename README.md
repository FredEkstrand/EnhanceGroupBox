![image](https://github.com/FredEkstrand/ImageFiles/raw/master/GroupBoxExamplesA.png)
# The Enhanced GroupBox Project

![Version 1.0.0](https://img.shields.io/badge/Version-1.0.0-brightgreen.svg) ![License MIT](https://img.shields.io/badge/Licence-MIT-blue.svg)

The Enhanced GroupBox control is a collection of features and styling to provide a richer UI control for application development.

## Features
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


## Download
The source code and provided installer is written in C# and targeted for the .Net Framework 4.0 and later. 
### Option 1:
A VSIX installer is created for installation into the toolbox for Visual Studio 2012, 2013, 2015, 2017 

Installer for the control can be found [here].

### Option 2:
Download the entire project and compile and add to the toolbox yourself.

## Usage
Use of the control is the same as with the basic Visual Studio GroupBox control with the exception of more option to change its appearance. In the designer property grid to keep option organized the Enhance GroupBox appearance section have subsection: Header, BorderItems, and InsideBorder. Below is an image showing the parts of the Enhance GroupBox control in relation to the property grid subsections.

![image](https://github.com/FredEkstrand/ImageFiles/raw/master/EnhanceGroupBoxParts.png)

In the image below is a screen shot of the property grid with the parts highlighted. 

![image](https://github.com/FredEkstrand/ImageFiles/raw/master/PropertiesGridView.png)

## Documentation
Code documentation can be found [here](http://fredekstrand.github.io/EnhanceGroupBox).

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
The project came about through the need in an application development to allow the user to easily and cleanly locate controls in defined sections. The Visual Studio standard GroupBox control is limited in setting its appearance to allow for an intuitive UI flow for the end user. From the initial quick extension which lead to further extensions of the control from project to projects had me take another look at a more formal enhance GroupBox control. In the development of the enhance group box control I had taken a survey of other group box development available on the web. From that survey I had taken some those ideas and incorporated them into this control.<br/>

Sources for project feature ideas:

**The Grouper - A Custom Groupbox Control by: MadHatter**

**Rounded GroupBox by: Kevin Carbis**

**Header-only GroupBox By: Matthew Adams**
