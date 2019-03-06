#!/bin/sh
cd ~/Desktop/
 
mv mimeapps.list .config/mimeapps.list
 
pactl set-sink-volume 0 50%
pactl set-sink-mute @DEFAULT_SINK@ toggle
 
git clone https://github.com/Lyx09/Immersion_pacman.git
git clone https://github.com/Lyx09/Immersion_p4clicker.git
 
#evince
