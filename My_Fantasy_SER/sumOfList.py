import sumOfPlayer


def main():
    """
    Add Documentation here
    """

    sumlst(["201933", "977", "201933"], "2015")

    pass  # Add Your Code Here


def sumlst(players, year):

    sumplayers = 0

    for i in players:
        sumplayers += sumOfPlayer.sumOne(i,year)

    return sumplayers


if __name__ == '__main__':
    main()