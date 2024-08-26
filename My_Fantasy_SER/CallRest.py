# -*- coding: utf-8 -*-
import requests
import json
from flask import jsonify

def main():
    """
    Add Documentation here
    """
    p = Nba()
    x = p.test("2018")
    print (x)
class Nba:
    def test(self, year):



        y = str(int(year)-1)

        r = 'http://data.nba.net/10s/prod/v1/' + y+"/players.json"
        req = requests.get(r)
        p = req.text

        data = json.loads(req.text)

        return req.text

    #print data

    lst = []







if __name__ == '__main__':
    main()