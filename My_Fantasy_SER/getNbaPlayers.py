
import CallRest
import datetime

class Players(object):
    """__init__() functions as the class constructor"""
    def __init__(self, season, updateTime, result):
        self.season = season
        self.updateTime = updateTime
        self.result = result


class CachedPlayers:
    myCachedPlayers = []



    def getCachedPlayesBySeason(self,season):

        t = CachedPlayers.myCachedPlayers

        for i in t:
            if i.season == season:
                deltaTime = datetime.datetime.now() - i.updateTime
                if deltaTime.days == 0:
                    return i.result
                else:
                    p = CallRest.Nba()
                    t = p.test(season)
                    i.result = t
                    i.updateTime = datetime.datetime.now()
                    return i.result


        p = CallRest.Nba()
        t = p.test(season)

        players = Players(season, datetime.datetime.now(), t)
        CachedPlayers.myCachedPlayers.append(players)

        return t


