"""
main file that invokes all the necessary procedures from these three files.
"""

import interface
from preprocessing import *
import annotation

if __name__ == '__main__':

    host, port, database, username, password, query = interface.GUI().initialise_GUI()
    connect = ConnectAndQuery(host, port, database, username, password)
    results = connect.getQueryPlan(query)
    
    annotation.draw(results, query)
    