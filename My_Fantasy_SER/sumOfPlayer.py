import requests
import json
import math


import getstats

def main():
    """
    Add Documentation here
    """

    sumOne("201933","2018")

    pass

def sumOne(personID,season):

    stats = getstats.stats(personID,season)
    if stats == "no information":
        print ("0 because no information")
        return 0
    ppg = float(stats["ppg"])
    if ppg == -1:
        print("0 because didnt play")
        return 0
    apg = float(stats["apg"])
    rpg = float(stats["rpg"])
    spg = float(stats["spg"])
    bpg = float(stats["bpg"])

    tot = int(ppg+apg+rpg+spg+bpg)


    print (tot)
    return tot


