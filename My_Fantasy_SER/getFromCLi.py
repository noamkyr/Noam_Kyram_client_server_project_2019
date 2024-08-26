# -*- coding: utf-8 -*-

from flask import Flask
import getstats
import sumOfPlayer
from flask import Flask
import tstLogin
import manageUsers
import manageTeams
import jsonpickle
import manageChallenge
from flask import jsonify
import manageChallenge
from manageChallenge import Challenge
import json
import sumOfList

import CallRest
from manageChallenge import MyChallenge
import getNbaPlayers


app = Flask(__name__)



lst = []
globalMyChallenge = MyChallenge()

"""@app.route("/")
def hello():
    return "Hello World!"


@app.route("/guy")
def helloGuy():
    return "Hello Guy!"""""

@app.route("/addChallenge/<user>/<pl1>/<pl2>/<pl3>/<pl4>/<pl5>/<nameTeam>/<Per1>/<Per2>/<year>")
def addChal(user, pl1,pl2,pl3,pl4,pl5,nameTeam,Per1,Per2,year):


    p = manageTeams.MyTeamsClass()

    inventor, y = p.addTeam(pl1, pl2, pl3, pl4, pl5, user, nameTeam, year)
    chal, status = globalMyChallenge.addNewChallenge(year, inventor, Per1, Per2)
    x = globalMyChallenge.getMyChals(Per1)
    x = globalMyChallenge.toJSON(x)
    return status

"""
@app.route('/getsum/<id>/<season>')
def show_sum(id,season):
    print ("id = " + id + " season = " + season)
    return str(sumOfPlayer.sumOne(id,season))
"""
@app.route('/addTeam/<idChal>/<pl1>/<pl2>/<pl3>/<pl4>/<pl5>/<teamName>/<user>')
def addTeamToChallenge(idChal, pl1, pl2, pl3, pl4, pl5, teamName, user):


    c = globalMyChallenge.getChalById(idChal)

    print("addTeamToChallenge, year = " + c.Team1.year)

    if c.Team1.user == user:
        c.Team1.players[0] = pl1
        c.Team1.players[1] = pl2
        c.Team1.players[2] = pl3
        c.Team1.players[3] = pl4
        c.Team1.players[4] = pl5
        c.Team1.name = teamName
        c.Team1.points = sumOfList.sumlst([pl1,pl2,pl3,pl4,pl5], c.Team1.year)
    else:
        c.Team2.players[0] = pl1
        c.Team2.players[1] = pl2
        c.Team2.players[2] = pl3
        c.Team2.players[3] = pl4
        c.Team2.players[4] = pl5
        c.Team2.name = teamName
        c.Team2.points = sumOfList.sumlst([pl1,pl2,pl3,pl4,pl5], c.Team2.year)
    t = manageTeams.MyTeamsClass()
    t.writeFile()
    globalMyChallenge.writeFile()
    return "Added"


@app.route('/user/<username>')
def show_user_profile(username):
    # show the user profile for that user
    v = manageUsers.Users()
    return v.addUser(username)

@app.route('/getUsers')
def getUsers():
    # returns all users in the game
    v = manageUsers.Users()

    y = v.getUsers()
    print(y)

    print("x = " +str(y))
    x = v.toJSON(y)

    m = jsonpickle.encode(y, unpicklable=False)
    print(m)
    return m


@app.route('/players/<season>')
def retAllPlayersByYear(season):

    x = getNbaPlayers.CachedPlayers()
    result = x.getCachedPlayesBySeason(season)

    return result

    p = CallRest.Nba()
    t = p.test(season)

    xx = str(y)

    print ("t = " + t)
    return t


@app.route('/challenges/<user>')
def getchals(user):
    v = MyChallenge()
    y = v.getMyChals(user)
    print (y)

    print("x = " +str(y))
    x = v.toJSON(y)

    m = jsonpickle.encode(y, unpicklable=False)
    print(m)
    return m


if __name__ == '__main__':

    globalMyChallenge.readFile()
    app.run(host="0.0.0.0")
