# -*- coding: utf-8 -*-
import json
import os

data = {
            "users": {
                "lstUsers": [
                ]
            }
        }

def main():
    """
    Add Documentation here
    """
    u = "yossi"
    print (isInData(u))
    print (isInData("Noam"))
    print (isInData("Orly"))

    print ("data = " + str(data))


def check(lst, usn):

    for i in lst:
        if i == usn:
            return True


    return False

def add(data, username):


    data["users"]["lstUsers"].append(username)


    return data


def isInData(username):
    p = "C:\\tstJson\\data.json"

    f = open(p, "r")
    #with open(p, "w") as f:
    fileContent = f.read()
    f.close()

    if fileContent == "":
        data = {
            "users": {
                "lstUsers": [
                ]
            }
        }

#        data = json.loads(firstData)
#        json.dump(firstData, f)
#        data = firstData
    else:
        data = json.loads(fileContent)




    print (data)

    if check(data["users"]["lstUsers"], username):
        return "inLst"
    else:
        data = add(data, username)
        #json.dump(data, f)
        return "added"

    return "good"

if __name__ == '__main__':
    main()