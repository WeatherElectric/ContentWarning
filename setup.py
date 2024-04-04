# -------------------------------------------------------------------------------
# MIT License
# 
# Copyright (c) 2023 Not Enough Photons & adamdev
# 
# Permission is hereby granted, free of charge, to any person obtaining a copy
# of this software and associated documentation files (the "Software"), to deal
# in the Software without restriction, including without limitation the rights
# to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
# copies of the Software, and to permit persons to whom the Software is
# furnished to do so, subject to the following conditions:
# 
# The above copyright notice and this permission notice shall be included in all
# copies or substantial portions of the Software.
# 
# THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
# IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
# FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
# AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
# LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
# OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
# SOFTWARE.
# -------------------------------------------------------------------------------

# Sets up the managed folder
# This makes it easier for other people to build and contribute

import sys
import os

cw_path = None
while True:
    cw_path = input("Where is Content Warning installed? ")

    if os.path.exists(cw_path):
        break
    else:
        print("Path '" + cw_path + "' does not exist! Do you have permissions to read it? Did you enter the path wrong?")

# Does the user have a MelonLoader folder?
bep_path = os.path.join(cw_path, "BepInEx")

if not os.path.exists(bep_path):
    print("BepInEx folder was not found! Have you installed BepInEx?")
    exit(1)

mod_path = os.path.join(bep_path, "plugins")

if not os.path.exists(bep_path):
    print("Plugins folder was not found! Have you launched BepInEx at least once?")
    exit(1)

# Then bridge these folders into a local "Links" folder with a hard link
print("Linking files and folders...")

if not os.path.exists("./Links"):
    os.mkdir("./Links")

os.symlink(mod_path, "./Links/Mods")
os.symlink(bep_path, "./Links/BepInEx")
os.symlink(cw_path, "./Links/Game")

print("Finding Content Warning executable...")

for file in os.listdir(cw_path):
    if file.endswith(".exe") and file.startswith("Content Warning"):
        print("Found '" + file + "'")
        os.symlink(os.path.join(cw_path, file), "./Links/Content Warning.exe")
        break