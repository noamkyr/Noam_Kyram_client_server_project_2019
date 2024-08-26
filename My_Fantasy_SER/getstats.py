import requests
import json
import pickle

def main():
    """
    Add Documentation here
    """
    stats("977","2015")


def stats(idPer,season):

    try:

        season = str(int(season)-1)

        print("season = " + season)
        r = "http://data.nba.net/10s/prod/v1/"+season+"/players/"+idPer+"_profile.json"
        print("r = " + r)
        req = requests.get(r)
        data = json.loads(req.text)

        return data["league"]["standard"]["stats"]["regularSeason"]["season"][0]["total"]

    except:
        return "no information"


if __name__ == '__main__':
    main()
