
# The Enhanced GroupBox Project

![Version 1.0.0](https://img.shields.io/badge/Version-1.0.0-brightgreen.svg) ![License MIT](https://img.shields.io/badge/Licence-MIT-blue.svg)

![image](https://github.com/FredEkstrand/ImageFiles/raw/master/GroupBox/GroupBoxHeader.png)

# Overview
The Enhanced GroupBox control is a collection of features and styling to provide a richer UI control for application development in windows Form.

## Features
The enhance groupbox is broken down into four styles. Each style have a set of features out line below:
##### 1. Standard Style
 *	Text color option.
 *	Borders color and width set.
 *	Borders corners can be rounded.
 *	Border rendering can have Dash styles.
 *	Border rendering can have LineCap styles.
 *	Text can be placed top left, top right, top center, bottom left, bottom right, and bottom center.
##### 2. Enhanced Style
 *	All the features listed in the Standard Style
 *	Text back color option.
 *	Text back gradient start and end color option with direction.
 *	Text border options similar to border options above.
 *	Inner border area can be set to a color.
 *	Inner border area can have a gradient start and end color option with direction.
##### 3. Executive Style
 * The Executive style is defined by the bar located on either the top or bottom with the text.
 *  Text color option.
 *	Border color and width set.
 * Text can be placed top left, top right, top center, bottom left, bottom right, and bottom center. With the restriction text is with the executive bar.
 *	Text back color option.
 *	Text back gradient start and end color option with direction.
 *	Text border options similar to border options above.
 *	Inner border area can be set to a color.
 *	Inner border area can have a gradient start and end color option with direction.
##### 4. Header Style
 * The Header style is defined by having a color background that extend across the top or bottom with the text inside.
 *  Text color option.
 *	Header color.
 *	Header gradient start and end color option with direction.
 *	Header border line with color option.
 * Text can be placed top left, top right, top center, bottom left, bottom right, and bottom center. With the restriction of text is inside header area.
 *	Inner border area can be set to a color.
 *	Inner border area can have a gradient start and end color option with direction.

# Getting Started
The source code is written in C# and targeted for the .Net Framework 4.0 and later.
You have two options to obtain the enhance groupbox.
### Option 1:
A VSIX installer is created for installation into the toolbox for Visual Studio 2012, 2013, 2015, 2017, 2019

{ No installer yet. }

### Option 2:
Download the entire project and compile and add to the toolbox yourself.

# Usage
Use of the control is similar to the basic Visual Studio GroupBox control with the exception of more options to change its appearance. To keep the additional options organized the property grid appearance section have subsection: HeaderElements, BorderElements, and InsideBorderElements. Below is an image showing the parts of the Enhance GroupBox control in relation to the property grid subsections.


![image](https://github.com/FredEkstrand/ImageFiles/raw/master/GroupBox/EnhanceGroupBoxParts.png)

In the image below is a screen shot of the property grid with the parts highlighted.

![image](https://github.com/FredEkstrand/ImageFiles/raw/master/GroupBox/PropertyGridView.png)

Below are some Enhance GroupBox configurations examples.
![image](https://github.com/FredEkstrand/ImageFiles/raw/master/GroupBox/GroupBoxSamples1.png)

{ Some code examples? }

# Code Documentation
MSDN-style code documentation [here](http://fredekstrand.github.io/EnhanceGroupBox).

# History
1.0.0 Initial release into the wild.

# Contributing

If you'd like to contribute, please fork the repository and use a feature
branch. Pull requests are always welcome.

# Contact
Fred Ekstrand
email: fredekstrandgithub@gmail.com

# Licensing
This project is licensed under the MIT License - see the LICENSE.md file for details.

# Acknowledgments
In creating the Enhanced GroupBox control ideas from other works where included to provide a range of styles and features.

##### Sources for project feature ideas:
**The Grouper** - A Custom Groupbox Control By: M@dHatter, 23 Jan 2006

**Header-only GroupBox** By: Matthew Adams, 22 Sep 2001

**RoundedGroupBox Winforms Rounded Group Box Control** By: Kevin Carbis

**CXPGroupBox** By: JackCa, 22 Apr 2004

# Source Code Included
**Extended Graphics** - Rounded rectangles, Font metrics and more for C# 3.0 By: Arun Reginald Zaheeruddin
