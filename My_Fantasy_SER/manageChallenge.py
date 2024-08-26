# -*- coding: utf-8 -*-
import flask
import requests
import pickle
import json
import json
import os.path
import pickle
import uuid
from json import JSONEncoder
from json import JSONDecoder
import sumOfList
import manageTeams
import manageUsers
from flask import jsonify

class Challenge:
    """__init__() functions as the class constructor"""
    def __init__(self, year, t1, t2, tInventor):

        self.status = 0
        self.challengeID = str(uuid.uuid4())
        self.creator = tInventor
        self.Team1 = t1
        self.Team2 = t2
        self.year = year


class MyChallenge:


    myChallenges = []

    def toJSON(self, onlymine):
        return json.dumps(onlymine, default=lambda o: o.__dict__,
            sort_keys=True, indent=4)

    def addChallenge(self,year, t1, t2, tInventor):

        newC = Challenge(year, t1, t2, tInventor)
        MyChallenge.myChallenges.append(newC)
        self.writeFile()
        return newC, "Added"

    def addNewChallenge(self, year, tInventor, USR1, USR2):

        p = manageTeams.MyTeamsClass()
        t1, y = p.addTeam("x","x","x","x","x",USR1,"x",year)
        t2, y2 = p.addTeam("x", "x", "x", "x", "x", USR2, "x", year)

        newC = Challenge(year, t1, t2, tInventor)
        MyChallenge.myChallenges.append(newC)
        self.writeFile()
        return newC, "Added"

    def isExist(self, nameTeam):
        for i in MyChallenge.myChallenges:
            if i.Team2.players[0] is "1":
                return True
                break

        return False


    def readFile(self):
        p = "C:\\tstJson\\Challenges.json"

        #global myChallenges
        if os.path.exists(p):
            m = open(p, "rb")
            if os.path.getsize(p) > 0:
                MyChallenge.myChallenges = pickle.load(m)
            else:
                MyChallenge.myChallenges = []
            m.close()
        else:
            print("not found")


    def getMyChals(self, userPar):
        lst = []

        t = MyChallenge.myChallenges
        for i in t:

            if i.Team2.user == userPar:
                lst.append(i)
            if i.Team1.user == userPar:
                lst.append(i)
            if i.creator.user == userPar:
                lst.append(i)
        return lst


    def writeFile(self):
        x = pickle.dumps(MyChallenge.myChallenges)
        p = "C:\\tstJson\\Challenges.json"
        g = open(p, "wb")
        g.write(x)
        g.close()

    def getChalById(self,chalId):
            for i in MyChallenge.myChallenges:
                if i.challengeID == chalId:
                    return i
            return


def main():
    """
    Add Documentation here
    """


    mynewChall = MyChallenge()

    mynewChall.readFile()

    myNewTeam = manageTeams.MyTeamsClass()


    x, y = myNewTeam.addTeam("1", "2", "3", "4", "5", "Noam", "myteam1","2018")
    x2, y2 = myNewTeam.addTeam("1", "james harden", "3", "4", "5", "idLeb", "myteam2","2018")
    x3, y3 = myNewTeam.addTeam("1", "2", "3", "4", "5", "Omri the goat", "myteam3","2018")

    chal, status = mynewChall.addChallenge("2012", x, x2, x3)

    print ("add = " + status, chal.Team2.players[1])


    #p = mynewChall.getMyChals("Noam")


    #printMyList()




    pass  # Add Your Code Here


if __name__ == '__main__':
    main()