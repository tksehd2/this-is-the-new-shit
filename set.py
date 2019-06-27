
def checkTrait(c):
    if '\n' in c:
        return False

    return (int((ord(c) - 0xAC00) % 28) != 0)


text = open('name_db', encoding='utf-8', errors='ignore')

items = text.readlines()
items = [item.replace('\n' , '') for item in items]
text.close()

merged = set(items)
result_list = []

for item in merged:
    if len(item) >= 4 or len(item) == 1:
        continue
    else:
        result_list.append("{}\n".format(item))

writer = open('name_db', mode='w', encoding='utf-8', errors='ignore')

writer.writelines(result_list)

writer.close()
