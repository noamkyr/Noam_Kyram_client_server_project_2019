import requests
import json
import CallRest


def main():
    """
    Add Documentation here
    """
    nametoid("James", "Harden", "2018")
    f, l = idToName("201935", "2018")


def nametoid(first, last, year):

    lst = CallRest.allPlayersInYear(year)
    tmp = []

    for i in lst:
        if i["firstName"] == first:
            tmp.append(i)


    #print lst
    #print tmp
    one = []
    for i in tmp:
        if i["lastName"] == last:
            one.append(i)
    print (one[0])
    print (len(one))
    print (one[0]["personId"])
    return one[0]["personId"]


def idToName(perId, year):

    lst = CallRest.allPlayersInYear(year)
    for i in lst:
        if i["personId"] == perId:
            break

    print (i["firstName"]+" "+i["lastName"])

    return i["firstName"], i["lastName"]




if __name__ == '__main__':
    main()