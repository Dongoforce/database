import random


with open("Monster.txt") as file:
    MonsterArr = [row.strip() for row in file]

with open("Susceptibility.txt") as file:
    SusceptibilityArr = [row.strip() for row in file]

with open("names.txt") as file:
    NameArr = [row.strip() for row in file]

skillLevel = ["beginner", "normal", "master", "professional"]

f = open("Monster.csv", "w")
g = open("Susceptibility.csv", "w")

f.write("Id,Name,ThraetLevel,Class,Suceptibility\n")
g.write("Id,Name,Type\n")


for i in range(1, len(MonsterArr) + 1):
    
    monster = random.choice(MonsterArr)
    string1 = "{0},{1}\n".format(i,monster)
    string2 = "{0},{1}\n".format(i, SusceptibilityArr[i-1])
    f.write(string1)
    g.write(string2)
    
g.close()
f.close()



f = open("Witcher.csv", "w")

f.write("Id,Name,SkillLevel,NumberOfKills\n")

for i in range(1, len(NameArr)):
    Name = random.choice(NameArr)
    Skill = random.choice(skillLevel)
    kills = 0
    if Skill == "beginner":
        kills = random.randint(0, 10)
    if Skill == "normal":
        kills = random.randint(10, 60)
    if Skill == "master":
        kills = random.randint(61, 200)
    if Skill == "professional":
        kills = random.randint(201, 1000)
    string = "{0},{1},{2},{3}\n".format(i, Name, Skill, kills)
    f.write(string)

f.close()

f = open("Order.csv", "w")

f.write("Id,WitcherId,MonsterId,CountOfMoney\n")

for i in range(1, len(NameArr)):
    WitcherId = random.randint(1, len(NameArr))
    MonsterId = random.randint(1, len(MonsterArr) )
    string = "{0},{1},{2},{3}\n".format(i, WitcherId, MonsterId, random.randint(10, 10000))
    f.write(string)

f.close()
